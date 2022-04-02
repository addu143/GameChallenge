using System.Collections.Generic;
using System.Linq;

namespace GameChallenge.Core.DBEntities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public double Total { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        Inactive,
        Active,

    }
}
