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
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IResponseGeneric _responseGeneric;
        private readonly ICustomerService _customerService;

        public OrderController(
            IMapper mapper,
            IConfiguration configuration,
            IResponseGeneric responseGeneric,
            IProductService productService,
            IOrderService orderService,
            ILogService logService,
            ICustomerService customerService) : base(customerService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _responseGeneric = responseGeneric;
            _productService = productService;
            _orderService = orderService;
            _logService = logService;
            _customerService = customerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderNewRequest orderRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region VALIDATIONS AND BASIC DATA
                    //Get current user:
                    var currentUser = GetCurrentUser();

                    //Group the records and sum quantity if dupplicate                    
                    var groupedRequestData = orderRequest.Products.ToList()
                        .GroupBy(m => m.ProductId)
                        .Select(m => new
                        {
                            ProductId = m.Key,
                            Quantity = m.Sum(x => x.Quantity)
                        });
                    
                    List<Product> listOfSelectedProducts = new List<Product>();

                    //Getting all products requested in memory
                    var productIds = groupedRequestData.Select(x => x.ProductId).ToArray();
                    var products = await _productService.GetByIdAsync(productIds);

                    foreach (var requestData in groupedRequestData)
                    {
                        //Verify product exist
                        var product = products.Where(m=> m.Id == requestData.ProductId).FirstOrDefault();
                        if (product == null)
                            return BadRequest(_responseGeneric.Error("Product not exist"));

                        //Quantity Check
                        if (requestData.Quantity > product.AvailableQuantity)
                            return BadRequest(_responseGeneric.Error(String.Format("Product # {0} not available, Available Quantity = {1}", product.Id, product.AvailableQuantity)));

                        listOfSelectedProducts.Add(product);
                    }
                    #endregion

                    #region OPERATIONS
                    Order order = new Order();
                    order.CustomerId = currentUser.Customer.Id;
                    order.OrderStatus = OrderStatus.Active;
                    order.OrderItems = new List<OrderItem>();

                    foreach (Product prod in listOfSelectedProducts)
                    {
                        var requestData = groupedRequestData.Where(m => m.ProductId == prod.Id).FirstOrDefault();
                        OrderItem orderItem = new OrderItem()
                        {
                            Price = prod.Price,
                            Product = prod,
                            Quantity = requestData.Quantity,
                            SubTotal = prod.Price * requestData.Quantity,
                            SKU = prod.SKU
                        };
                        order.OrderItems.Add(orderItem);
                    }
                    order.Total = order.OrderItems.Sum(t => t.SubTotal);

                    //Save to DB and also handling the Concurrency issue:
                    await _orderService.CreateOrderAndUpdateStockAsync(order);
                    #endregion

                    //Logging
                    await _logService.InsertLog(LogLevel.Information, "Order has successfully created", JsonSerializer.Serialize(orderRequest));

                    return Ok(_responseGeneric.Success("Order has successfully created"));
                }
                else
                {
                    return BadRequest(_responseGeneric.Error(result: ModelState));
                }

            }
            catch (Exception ex)
            {
                //Logging error
                Log logDetail = _logService.InsertLog(LogLevel.Error, "CreateOrder", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }

        [HttpGet("getCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrders()
        {
            try
            {
                //Logging
                await _logService.InsertLog(LogLevel.Information, "GetCustomerOrders");

                var currentUser = GetCurrentUser();
                var orders = await _orderService.GetCustomerOrdersAsync(currentUser.Customer.Id);
                var orderMapp = _mapper.Map<List<OrderListResponse>>(orders);
                return Ok(_responseGeneric.Success(result: orderMapp));
            }
            catch (Exception ex)
            {
                //Logging error
                Log logDetail = _logService.InsertLog(LogLevel.Error, "GetCustomerOrders", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }

        [HttpGet("getCustomerOrderDetail")]
        public async Task<IActionResult> GetCustomerOrderDetail([FromBody] OrderDetailRequest orderRequest)
        {
            try
            {
                //Logging
                await _logService.InsertLog(LogLevel.Information, "GetCustomerOrderDetail");

                var currentUser = GetCurrentUser();
                var order = await _orderService.GetCustomerOrderDetailAsync(currentUser.Customer.Id, orderRequest.OrderId);
                var orderMapp = _mapper.Map<OrderDetailResponse>(order);
                return Ok(_responseGeneric.Success(result: orderMapp));
            }
            catch (Exception ex)
            {
                //Logging error
                Log logDetail = _logService.InsertLog(LogLevel.Error, "GetCustomerOrderDetail", ex.ToString()).Result;
                return BadRequest(_responseGeneric.Error(result: ex.Message.ToString(), log: logDetail));
            }
        }
    }
}
