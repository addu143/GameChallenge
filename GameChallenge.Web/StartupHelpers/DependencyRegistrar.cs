using Microsoft.Extensions.DependencyInjection;
using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using GameChallenge.Core.Services;
using GameChallenge.Infrastructure.Data;
using GameChallenge.Web.EnpointModel;
using GameChallenge.Web.Helpers;
using System;

namespace GameChallenge.Web
{
    internal class DependencyRegistrar
    {
        private IServiceCollection _services;

        public DependencyRegistrar(IServiceCollection services)
        {
            this._services = services;
        }

        internal void ConfigureDependencies()
        {

            _services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
            _services.AddScoped<IRepository<ProductCategory>, EfRepository<ProductCategory>>();
            _services.AddScoped<IRepository<Product>, EfRepository<Product>>();
            _services.AddScoped<IRepository<Order>, EfRepository<Order>>();
            _services.AddScoped<IRepository<Log>, EfRepository<Log>>();

            _services.AddScoped<ICustomerService, CustomerService>();
            _services.AddScoped<IProductService, ProductService>();
            _services.AddScoped<IOrderService, OrderService>();
            _services.AddScoped<ILogService, LogService>();

            _services.AddScoped<IResponseGeneric, ResponseGeneric>();



        }
    }
}