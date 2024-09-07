using Bhank.Customer.Api.Application.DTOs;
using Bhank.Customer.Api.Application.Interfaces;
using Bhank.Customer.Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bhank.Customer.Api.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase, ICustomerController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(
            ICustomerService customerService
        ) {
            _customerService = customerService;
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
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCustomer = await _customerService.CreateCustomerAsync(customerDTO);
            return CreatedAtAction(nameof(CreateCustomer), new { id = createdCustomer.Id }, createdCustomer);
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
        public async Task<IActionResult> GetCustomerById([FromQuery]Guid customerId)
        {
            if(customerId == Guid.Empty)
                return BadRequest("Invalid customer ID");
                
            var customer = await _customerService.GetCustomerAsync(customerId);
            return Ok(customer);
        }
    }
}