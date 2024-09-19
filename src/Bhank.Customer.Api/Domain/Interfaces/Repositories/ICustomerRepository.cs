using Bhank.Customer.Api.Domain.Entities;

namespace Bhank.Customer.Api.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetCustomerByIdAsync(Guid id);
        Task<List<CustomerEntity>> GetAllCustomersAsync();
        Task<CustomerEntity> CreateCustomerAsync(CustomerEntity customer);
        Task<CustomerEntity> UpdateCustomerAsync(Guid id, CustomerEntity customer);
        Task<AddressEntity> UpdateCustomerAddressAsync(Guid id, AddressEntity address);
        Task<bool> ActivateCustomer(Guid id);
        Task<bool> InactivateCustomer(Guid id);
    }
}