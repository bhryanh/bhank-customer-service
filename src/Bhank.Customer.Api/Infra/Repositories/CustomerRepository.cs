using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;

namespace Bhank.Customer.Api.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _dbContext;
        private readonly ILogger<ICustomerRepository> _logger;

        public CustomerRepository(
            CustomerContext dbContext,
            ILogger<ICustomerRepository> logger
        )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CustomerEntity> CreateCustomerAsync(CustomerEntity customer)
        {
            try
            {
                var customerCreated = _dbContext.Add(customer);
                await _dbContext.SaveChangesAsync();
                return customerCreated.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                throw;
            }
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Customers.FindAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting customer by id");
                throw;
            }
        }

        public async Task<bool> ActivateCustomer(Guid id)
        {
            try
            {
                var customer = await _dbContext.Customers.FindAsync(id);
                if (customer == null)
                    return false;

                customer.Activate();
                _dbContext.Update(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error activating customer");
                throw;
            }
        }

        public async Task<bool> InactivateCustomer(Guid id)
        {
            try
            {
                var customer = await _dbContext.Customers.FindAsync(id);
                if (customer == null)
                    return false;

                customer.Inactivate();
                _dbContext.Update(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inactivating customer");
                throw;
            }
        }
    }
}