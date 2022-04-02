using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Electronics"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1, ProductCategoryId = 1, Name="IPhone", Quantity=100, SKU="IPP", Sold=0, Price=1100 },
                new Product { Id = 2, ProductCategoryId = 1, Name = "Samsung A20", Quantity = 200, SKU = "S20", Sold = 0, Price = 2000 },
                new Product { Id = 3, ProductCategoryId = 1, Name = "Guitar", Quantity = 300, SKU = "GUI", Sold = 0, Price = 500 },
                new Product { Id = 4, ProductCategoryId = 1, Name = "Microsoft Keyboard", Quantity = 50, SKU = "MK", Sold = 0, Price = 200 },
                new Product { Id = 5, ProductCategoryId = 1, Name = "Washing Machine", Quantity = 50, SKU = "WMA", Sold = 0, Price = 877 }
            );
        }
    }
}
