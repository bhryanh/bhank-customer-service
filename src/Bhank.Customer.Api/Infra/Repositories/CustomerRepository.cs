using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CustomerEntity> UpdateCustomerAsync(Guid id, CustomerEntity customerEntity)
        {
            try
            {
                var customer = await _dbContext.Customers.FindAsync(id);
                if (customer == null)
                    return null;
                
                if (!string.IsNullOrWhiteSpace(customerEntity.FirstName))
                    customer.FirstName = customerEntity.FirstName;

                if (!string.IsNullOrWhiteSpace(customerEntity.LastName))
                    customer.LastName = customerEntity.LastName;

                if (!string.IsNullOrWhiteSpace(customerEntity.Email))
                    customer.Email = customerEntity.Email;

                if (!string.IsNullOrWhiteSpace(customerEntity.PhoneNumber))
                    customer.PhoneNumber = customerEntity.PhoneNumber;

                if (customer.DateOfBirth.HasValue)
                    customer.DateOfBirth = customerEntity.DateOfBirth.Value;

                if (!string.IsNullOrWhiteSpace(customerEntity.Nationality))
                    customer.Nationality = customerEntity.Nationality;

                customer.UpdatedAt = DateTime.UtcNow;

                var customerUpdated = await _dbContext.SaveChangesAsync();

                if(customerUpdated > 0)
                    return customer;
                return null;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating customer");
                throw;
            }
        }

        public async Task<AddressEntity> UpdateCustomerAddressAsync(Guid id, AddressEntity addressEntity)
        {
            try
            {
                var customer = await _dbContext.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
                if (customer == null || customer.Address == null)
                    return null;
                
                if (!string.IsNullOrWhiteSpace(addressEntity.Street))
                    customer.Address.Street = addressEntity.Street;

                if (!string.IsNullOrWhiteSpace(addressEntity.City))
                    customer.Address.City = addressEntity.City;

                if (!string.IsNullOrWhiteSpace(addressEntity.State))
                    customer.Address.State = addressEntity.State;

                if (!string.IsNullOrWhiteSpace(addressEntity.Country))
                    customer.Address.Country = addressEntity.Country;

                if (!string.IsNullOrWhiteSpace(addressEntity.PostalCode))
                    customer.Address.PostalCode = addressEntity.PostalCode;

                customer.Address.UpdatedAt = DateTime.UtcNow;

                var customerUpdated = await _dbContext.SaveChangesAsync();

                if(customerUpdated > 0)
                    return customer.Address;
                return null;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating customer address");
                throw;
            }
        }
    }
}