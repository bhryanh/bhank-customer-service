using Bhank.Customer.Api.Domain.Entities;

namespace Bhank.Customer.Api.Domain.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<AddressEntity> GetAddressByIdAsync(Guid id);
        Task<AddressEntity> CreateAddressAsync(AddressEntity address);
    }
}