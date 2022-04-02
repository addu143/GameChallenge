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
    public class OrderService : IOrderService
    {
        private IRepository<Order> _repository;
        IProductService _productService;

        public OrderService(IRepository<Order> repository,
            IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        public Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            return _repository.AddAsync(order, cancellationToken);
        }

        public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            return _repository.UpdateAsync(order, cancellationToken);
        }

        public Task DeleteAsync(Order order, CancellationToken cancellationToken = default)
        {
            return _repository.DeleteAsync(order, cancellationToken);
        }

        public Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _repository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _repository.ListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _repository.Table
                .Where(m => m.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order> GetCustomerOrderDetailAsync(int customerId, int orderId, CancellationToken cancellationToken = default)
        {
            return await _repository.Table
                .Where(m => m.CustomerId == customerId && m.Id == orderId)
                .Include(m => m.OrderItems)
                .ThenInclude(m => m.Product)
                .ThenInclude(m=> m.ProductCategory)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Order> CreateOrderAndUpdateStockAsync(Order order, CancellationToken cancellationToken = default)
        {
            using (var transation = _repository.BeginTransaction())
            {
                //Update Order
                await this.AddAsync(order);

                //Update Stock
                foreach (var orderItem in order.OrderItems)
                {
                    bool isSuccess = await _productService.RecheckAndUpdateStock(orderItem.Product, orderItem.Quantity);
                    if (!isSuccess)
                        throw new Exception("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                await transation.CommitAsync();
                return order;
            }
        }

    }
}
