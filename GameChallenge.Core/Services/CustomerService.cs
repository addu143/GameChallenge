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
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerService(IRepository<Customer> repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users.Where(m => m.Email == email).Include(m => m.Customer).FirstOrDefaultAsync(cancellationToken);            
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(customer, cancellationToken);
        }

        public async Task<List<Customer>> ListOfCustomersAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

       
    }
}
