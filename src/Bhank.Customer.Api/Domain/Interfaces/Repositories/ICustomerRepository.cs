using Bhank.Customer.Api.Domain.Entities;

namespace Bhank.Customer.Api.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetCustomerByIdAsync(Guid id);
        CustomerEntity CreateCustomer(CustomerEntity customer);
    }
}