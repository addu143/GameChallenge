using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameChallenge.Web.EnpointModel
{
    public class PlayerRegisterRequest
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)] 
        public string Password { get; set; }
    }

    public class PlayerLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }

    public class PlayerLoginResponse
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }
    }

    public class PlayerChallengeRequest
    {
        [Required]
        public int Points { get; set; }

        [Required]
        public int Number { get; set; }
    }

    public class PlayerChallengeResponse
    {
        public string PlayerEmail { get; set; }

        public int Account { get; set; }

        public string Status { get; set; }

        public int Points { get; set; }
    }
}
