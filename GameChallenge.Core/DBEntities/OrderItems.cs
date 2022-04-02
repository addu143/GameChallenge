namespace GameChallenge.Core.DBEntities
{
    public class OrderItem : BaseEntity
    {
        public string SKU { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public double SubTotal { get; set; }

    }





}
