using System.Linq.Expressions;
using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bhank.Customer.Api.Infra.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerContext _context;
        private readonly ILogger<IAddressRepository> _logger;

        public AddressRepository(
            CustomerContext context,
            ILogger<IAddressRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        
        public async Task<AddressEntity> CreateAddressAsync(AddressEntity address)
        {
            try
            {
                var addressCreated = _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                return addressCreated.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating address");
                throw;
            }
        }

        public async Task<AddressEntity> GetAddressByIdAsync(Guid id)
        {
            try 
            {
                return await _context.Addresses.FindAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting address by id");
                throw;
            }
        }
    }
}