using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameChallenge.Core.DBEntities
{
    public class Product : BaseEntity
    {
        [MaxLength(500)]
        public string Name { get; set; }

        public virtual int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        [MaxLength(10)]
        public string SKU { get; set; }

        public int Quantity { get; set; }
        
        public int Sold { get; set; }

        public double Price { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
               
        public int AvailableQuantity
        {
            get
            {
                return Quantity - Sold;
            }
            set { }
        }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }





}
