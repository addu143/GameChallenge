using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using GameChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Services
{
    public class ProductService : IProductService
    {
        private IRepository<ProductCategory> _repositoryProductCategory;
        private IRepository<Product> _repositoryProduct;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductService(IRepository<Product> repositoryProduct,
            IRepository<ProductCategory> repositoryProductCategory,
            UserManager<ApplicationUser> userManager)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryProductCategory = repositoryProductCategory;
            _userManager = userManager;
        }

        public Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.AddAsync(product, cancellationToken);
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.UpdateAsync(product, cancellationToken);
        }

        public Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.DeleteAsync(product, cancellationToken);
        }

        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.Table
                .Include(m => m.ProductCategory)
                .ToListAsync();
        }

        public Task<List<ProductCategory>> GetCategoryAllAsync(CancellationToken cancellationToken = default)
        {
            return _repositoryProductCategory.ListAsync(cancellationToken);
        }

        public Task<ProductCategory> AddCategoryAsync(ProductCategory productCategory, CancellationToken cancellationToken = default)
        {
            return _repositoryProductCategory.AddAsync(productCategory, cancellationToken);
        }

        public Task<List<Product>> GetByIdAsync(int[] id, CancellationToken cancellationToken = default)
        {
            return _repositoryProduct.Table.Where(m => id.Contains(m.Id)).ToListAsync();
        }

        public async Task<bool> RecheckAndUpdateStock(Product product, int askQuantity)
        {
            bool saveFailed = false;
            int numberOfTries = 0;         

            //Update Product Quantity and also logic to handle Concorrency issues:
            do
            {
                try
                {
                    numberOfTries++;

                    //Check Again quantity
                    if (askQuantity <= product.AvailableQuantity)
                        product.Sold += askQuantity;
                    else
                        return false;

                    //Update the Product
                    await _repositoryProduct.UpdateAsync(product);

                    saveFailed = false;
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //If concurrency issue occured
                    saveFailed = true;
                    product = await _repositoryProduct.GetByIdAsync(product.Id);
                }

            } while (saveFailed || numberOfTries < 5);

            return false;
            //}
        }



        //public bool CheckProductQuantity(List<Product> products, CancellationToken cancellationToken = default)
        //{

        //    bool isNotAvailable = _repositoryProduct
        //        .Table
        //        .Where(m => products.Any(local => local.Id == m.Id 
        //               && local.Quantity <= 0)).Any();

        //    return isNotAvailable;
        //}
    }
}
