using System.ComponentModel.DataAnnotations;

namespace GameChallenge.Web.EnpointModel
{
    public class CreateProductCategoryRequest
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
    }

    public class CreateProductRequest
    {    
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }
        
        [MaxLength(10)]
        public string SKU { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Sold { get; set; }

        [Required]
        public double Price { get; set; }
    }

    public class ProductCategoryResponse
    {
        public string Name { get; set; }
    }
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }                
        public ProductCategoryResponse ProductCategory { get; set; }       
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }

    }

    public class UpdateProductRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Number")]
        public double Price { get; set; }
    }

}
