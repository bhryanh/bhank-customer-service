using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;

namespace Bhank.Customer.Api.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _dbContext;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(
            CustomerContext dbContext,
            ILogger<CustomerRepository> logger
        )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public CustomerEntity CreateCustomer(CustomerEntity customer)
        {
            try
            {
                var customerCreated = _dbContext.Add(customer);
                _dbContext.SaveChanges();
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
            return await _dbContext.Customers.FindAsync(id);
        }
    }
}