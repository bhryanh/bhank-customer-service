using AutoMapper;
using Bhank.Customer.Api.Application.DTOs;
using Bhank.Customer.Api.Application.Interfaces;
using Bhank.Customer.Api.Domain.Entities;
using Bhank.Customer.Api.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bhank.Customer.Api.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase, ICustomerController
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerController(
            ILogger<CustomerController> logger,
            ICustomerRepository customerRepository,
            IMapper mapper
        ) {
            _logger = logger;
            _customerRepository = customerRepository;
            _mapper = mapper;
         }

        
        /// <summary>
        /// This endpoint creates a new customer in the system.
        /// </summary>
        /// <param name="customer">The customer object to be created.</param>
        /// <returns>The newly created customer.</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the customer is invalid</response>
        [HttpPost]
        [ProducesResponseType<CustomerDTO>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            var customer = _customerRepository.CreateCustomer(_mapper.Map<CustomerEntity>(customerDTO));
            
            if (customer != null)
                return Ok(_mapper.Map<CustomerDTO>(customer));
            
            return BadRequest("Error creating customer");
        }

        /// <summary>
        /// Retrieves a specific customer by unique ID.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The customer with the specified ID.</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        [HttpGet]
        [ProducesResponseType<CustomerDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerById([FromQuery]Guid clientId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(clientId);
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }
    }
}