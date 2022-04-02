using GameChallenge.Core.DBEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameChallenge.Web.EnpointModel
{
    public class OrderNewRequest
    {
        [Required, MinLength(1)]
        public OrderNewProductRequest[] Products { get; set; }
    }

    public class OrderNewProductRequest 
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }

    }

    public class OrderListResponse
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int CustomerId { get; set; }        
    }
    public class OrderDetailRequest
    {
        public int OrderId { get; set; }
    }
    public class OrderDetailResponse
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int CustomerId { get; set; }
        public virtual List<OrderItemResponse> OrderItems { get; set; }
    }

    public class OrderItemResponse 
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public double SubTotal { get; set; }
        public ProductResponse Product { get; set; }

    }

}
