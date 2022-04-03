using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using GameChallenge.Web.EnpointModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GameChallenge.Common.Helpers;
using GameChallenge.Common.Numbers;
using System.Linq;

namespace GameChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IResponseGeneric _responseGeneric;
        private readonly ILogService _logService;
        private readonly IWorkContext _workContext;
        private readonly INumberHelper _numberHelper;
        private readonly IPlayerCalculations _playerCalculation;
        private readonly ISettingService _settingService;

        public PlayerController(
            IPlayerService customerService,
            IMapper mapper,
            IConfiguration configuration,
            IResponseGeneric responseGeneric,
            ILogService logService,
            IWorkContext workContext,
            INumberHelper numberHelper,
            IPlayerCalculations playerCalculation,
            ISettingService settingService)
        {
            _playerService = customerService;
            _mapper = mapper;
            _configuration = configuration;
            _responseGeneric = responseGeneric;
            _logService = logService;
            _workContext = workContext;
            _numberHelper = numberHelper;
            _playerCalculation = playerCalculation;
            _settingService = settingService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PlayerRegisterRequest cusModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Registering User", JsonSerializer.Serialize(cusModel));

                    #region VALIDATIONS
                    var isAlreadyExist = await _playerService.FindByEmailAsync(cusModel.Email) != null;
                    if (isAlreadyExist)
                    {
                        return BadRequest(_responseGeneric.Error("User already exist with this email address, Please try another one."));
                    }
                    #endregion

                    #region OPERATIONS
                    ApplicationUser user = new ApplicationUser()
                    {
                        Email = cusModel.Email,
                        UserName = cusModel.Email,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _playerService.CreateAsync(user, cusModel.Password);
                    if (!result.Succeeded)
                    {
                        List<string> errors = new List<string>();
                        if (result.Errors != null)
                        {
                            foreach (var error in result.Errors)
                            {
                                errors.Add(error.Description.ToString());
                            }
                        }

                        return BadRequest(_responseGeneric.Error(result: errors));
                    }

                    //Create an entry in Customer Table along with ASPNET Identity tables:

                    //Creating by default 10000 points for a new user:
                    var playerBets = new List<PlayerBet>();
                    playerBets.Add(new PlayerBet() { Comment = "Default points/money on registration", Amount = 10000 });
                    await _playerService.AddAsync(new Player()
                    {
                        ApplicationUser = user,
                        Name = cusModel.Name,
                        PlayerBets = playerBets
                    });

                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Registering Success User", JsonSerializer.Serialize(cusModel));

                    return Ok(_responseGeneric.Success());
                    #endregion

                    
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));

                }

            }
            catch (Exception ex)
            {
                //Logging
                await _logService.InsertLog(LogLevel.Error, "Registering User", ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] PlayerLoginRequest loginModel)
        {
            try
            {
                //Logging
                await _logService.InsertLog(LogLevel.Information, "Try Logging", JsonSerializer.Serialize(loginModel));

                var user = await _playerService.FindByEmailAsync(loginModel.Email);
                if (user != null && await _playerService.CheckPasswordAsync(user, loginModel.Password))
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("CustomerID", user.Player.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(_responseGeneric.Success(result: new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }));
                }
                return Unauthorized(_responseGeneric.Error("Wrong credentials"));
            }
            catch (Exception ex)
            {
                //Logging
                await _logService.InsertLog(LogLevel.Error, "Logging User", ex.ToString());
                throw;
            }
           
        }

        [Authorize]
        [HttpPost]        
        [Route("challenge")]
        public async Task<IActionResult> Challenge([FromBody] PlayerChallengeRequest challengeRequest)
        {
            try
            {
                #region BASIC DATA and VALIDATIONS
                var currentPlayer = _workContext.CurrentPlayer.Player;
                int availablePoints = _playerCalculation.AvailablePoints(currentPlayer);

                //Getting data from settings table:
                List<Setting> listOfSettings = await _settingService.ListOfSettingsAsync();
                int randomNumberMinDb = Convert.ToInt32(listOfSettings.Find(m => m.Name == "RandomNumberMin").Value);
                int randomNumberMaxDb = Convert.ToInt32(listOfSettings.Find(m => m.Name == "RandomNumberMax").Value);
                int rewardIncreaseByTimes = Convert.ToInt32(listOfSettings.Find(m => m.Name == "RewardHowManyTimes").Value);
                /////

                if (availablePoints <= 0)
                {
                    return BadRequest(_responseGeneric.Error("You are out of funds"));
                }
                if (challengeRequest.Points <= 0)
                {
                    return BadRequest(_responseGeneric.Error("Point value should be a positive number."));
                }
                if (challengeRequest.Number < randomNumberMinDb || challengeRequest.Number > randomNumberMaxDb)
                {
                    return BadRequest(_responseGeneric.Error($"Number should be between {randomNumberMinDb} and {randomNumberMaxDb}"));
                }
                #endregion

                #region OPERATIONS
                int randomNumberBySystem = _numberHelper.GenerateRandomNumber(0, 10);
                int pointsGain = _playerCalculation.Challenge(randomNumberBySystem,
                    challengeRequest.Points,
                    challengeRequest.Number,
                    rewardIncreaseByTimes);

                PlayerBet bet = new PlayerBet()
                {
                    RandomNumberByUser = challengeRequest.Number,
                    RandomNumberBySystem = randomNumberBySystem,
                    Player = currentPlayer,
                    Comment = "Played by user",
                    Amount = pointsGain,
                };
                currentPlayer.PlayerBets.Add(bet);

                //Update,Save to Database:
                await _playerService.UpdateAsync(currentPlayer);

                return Ok(_responseGeneric.Success(result: new PlayerChallengeResponse()
                {
                    PlayerEmail = _workContext.CurrentPlayer.Email,
                    Account = _playerCalculation.AvailablePoints(currentPlayer),
                    Status = pointsGain >= 0 ? "won" : "lose",
                    Points = pointsGain
                }));
                #endregion
            }
            catch (Exception ex)
            {
                //Logging
                await _logService.InsertLog(LogLevel.Error, "Challenge Method", ex.ToString());
                throw;
            }
     
        }

        [Authorize]
        [HttpPost]
        [Route("availablePoints")]
        public async Task<IActionResult> AvailablePoints()
        {
            try
            {
                var currentPlayer = _workContext.CurrentPlayer.Player;
                int availablePoints = _playerCalculation.AvailablePoints(currentPlayer);
                return Ok(_responseGeneric.Success(result: new
                {
                    playerEmail = _workContext.CurrentPlayer.Email,
                    availablePoints = availablePoints
                }));
            }
            catch (Exception ex)
            {
                //Logging
                await _logService.InsertLog(LogLevel.Error, "AvailablePoints Method", ex.ToString());
                throw;
            }
          
        }

    }
}
