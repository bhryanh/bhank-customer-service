using Bhank.Customer.Api.Domain.Entities;

namespace Bhank.Customer.Api.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetCustomerByIdAsync(Guid id);
        Task<CustomerEntity> CreateCustomerAsync(CustomerEntity customer);
        Task<bool> ActivateCustomer(Guid id);
        Task<bool> InactivateCustomer(Guid id);
    }
}