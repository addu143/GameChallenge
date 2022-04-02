using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Core.DBEntities
{
    public class ProductCategory : BaseEntity
    {
        [MaxLength(500)]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
