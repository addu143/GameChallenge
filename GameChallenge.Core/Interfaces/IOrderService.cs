using GameChallenge.Core.DBEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task DeleteAsync(Order order, CancellationToken cancellationToken = default);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Order>> GetCustomerOrdersAsync(int customerId, CancellationToken cancellationToken = default);
        Task<Order> CreateOrderAndUpdateStockAsync(Order order, CancellationToken cancellationToken = default);
        Task<Order> GetCustomerOrderDetailAsync(int customerId, int orderId, CancellationToken cancellationToken = default);
    }
}
