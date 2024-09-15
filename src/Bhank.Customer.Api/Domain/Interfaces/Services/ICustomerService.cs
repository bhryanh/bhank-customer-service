using Bhank.Customer.Api.Application.DTOs;
using Bhank.Customer.Api.Domain.Entities;

namespace Bhank.Customer.Api.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<CustomerEntity> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<CustomerDTO> GetCustomerAsync(Guid id);
        Task<bool> ActivateCustomer(Guid id);
        Task<bool> InactivateCustomer(Guid id);
    }
}