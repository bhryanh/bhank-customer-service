using AutoMapper;
using Bhank.Customer.Api.Application.DTOs;
using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;
using Bhank.Customer.Api.Domain.Interfaces.Services;

namespace Bhank.Customer.Api.Application.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ICustomerService> _logger;

        public CustomerService(
            ICustomerRepository customerRepository,
            IAddressRepository addressRepository,
            IMapper mapper,
            ILogger<ICustomerService> logger)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerEntity> CreateCustomerAsync(CustomerDTO customerDto)
        {
            var addressEntity = _mapper.Map<AddressEntity>(customerDto.Address);

            var createdAddress = await _addressRepository.CreateAddressAsync(addressEntity);

            var customerEntity = _mapper.Map<CustomerEntity>(customerDto);

            customerEntity.Address = createdAddress;
            customerEntity.AddressId = createdAddress.Id;

            return await _customerRepository.CreateCustomerAsync(customerEntity);
        }

        public async Task<CustomerDTO> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<bool> ActivateCustomer(Guid id)
        {
            return await _customerRepository.ActivateCustomer(id);
        }

        public async Task<bool> InactivateCustomer(Guid id)
        {
            return await _customerRepository.InactivateCustomer(id);
        }
    }
}