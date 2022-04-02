using GameChallenge.Core.DBEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetByIdAsync(int[] id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<ProductCategory>> GetCategoryAllAsync(CancellationToken cancellationToken = default);
        Task<ProductCategory> AddCategoryAsync(ProductCategory productCategory, CancellationToken cancellationToken = default);
        Task<bool> RecheckAndUpdateStock(Product product, int askQuantity);
    }
}
