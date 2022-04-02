using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using GameChallenge.Web.EnpointModel;
using GameChallenge.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;
        private readonly IResponseGeneric _responseGeneric;
        private readonly ILogService _logService;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            IConfiguration configuration, 
            IResponseGeneric responseGeneric,
            ILogService logService)
        {
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
            _responseGeneric = responseGeneric;
            _logService = logService;
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest updateRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "UpdateProduct");

                    #region VALIDATIONS
                    var product = await _productService.GetByIdAsync(updateRequest.ProductId);
                    if(product == null) return NotFound(_responseGeneric.Error("Product not found"));
                    #endregion

                    #region OPERATIONS
                    product.Name = updateRequest.Name;
                    product.Quantity = updateRequest.Quantity;
                    product.Price = updateRequest.Price;
                    await _productService.UpdateAsync(product);
                    #endregion
                   
                    return Ok(_responseGeneric.Success("Product has updated successfully"));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging
                Log logDetail = _logService.InsertLog(LogLevel.Error, "UpdateProduct", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }


        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "GetProducts");

                    var products = await _productService.GetAllAsync();
                    List<ProductResponse> listOfProducts = _mapper.Map<List<ProductResponse>>(products);                    
                    return Ok(_responseGeneric.Success(result: listOfProducts));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging
                Log logDetail = _logService.InsertLog(LogLevel.Error, "GetProducts", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }
    }
}
