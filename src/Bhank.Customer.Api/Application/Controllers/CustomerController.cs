using Bhank.Customer.Api.Application.DTOs;
using Bhank.Customer.Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bhank.Customer.Api.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
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
        [HttpGet("{customerId}")]
        [ProducesResponseType<CustomerDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            if(customerId == Guid.Empty)
                return BadRequest("Invalid customer ID");
                
            var customer = await _customerService.GetCustomerAsync(customerId);
            return Ok(customer);
        }

        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <returns>A list of customers.</returns>
        /// <response code="200">Returns the list of customers</response>
        [HttpGet]
        [ProducesResponseType<List<CustomerDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }


        /// <summary>
        /// Update customer with a given Id of the Customer
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The customer updated</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        /// <response code="400">If the id is invalid</response>
        [HttpPatch("{customerId}")]
        [ProducesResponseType<IActionResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody]UpdateCustomerDTO customerDTO)
        {
            if(customerId == Guid.Empty)
                return BadRequest(new { message = "Invalid Customer Id" });

            if(customerDTO == null)
                return BadRequest(new { message = "Invalid Customer Data" });

            var result = await _customerService.UpdateCustomerAsync(customerId, customerDTO);

            if(result == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(customerDTO);
        }

        /// <summary>
        /// Update customer address with a given Id of the Customer
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The adddress updated</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        /// <response code="400">If the id is invalid</response>
        [HttpPatch("{customerId}/address")]
        [ProducesResponseType<IActionResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomerAddress(Guid customerId, [FromBody]AddressDTO addressDTO)
        {
            if(customerId == Guid.Empty)
                return BadRequest(new { message = "Invalid Customer Id" });

            if(addressDTO == null)
                return BadRequest(new { message = "Invalid Address Data" });

            var result = await _customerService.UpdateCustomerAddressAsync(customerId, addressDTO);

            if(result == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(addressDTO);
        }

        /// <summary>
        /// Activate customer with a given Id
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>A message if the Customer was activated</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        /// <response code="400">If the id is invalid</response>
        [HttpPut("{customerId}/activate")]
        [ProducesResponseType<IActionResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActivateCustomer(Guid customerId)
        {
            if(customerId == Guid.Empty)
                return BadRequest(new { message = "Invalid Customer Id" });

            var result = await _customerService.ActivateCustomer(customerId);

            if(!result)
                return NotFound(new { message = "Customer not found" });

            return Ok(new { message = "Customer activated succesfully" });
        }

        /// <summary>
        /// Activate customer with a given Id
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>A message if the Customer was inactivated</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        /// <response code="400">If the id is invalid</response>
        [HttpPut("{customerId}/inactivate")]
        [ProducesResponseType<IActionResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IActionResult>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InactivateCustomer(Guid customerId)
        {
            if(customerId == Guid.Empty)
                return BadRequest(new { message = "Invalid Customer Id" });

            var result = await _customerService.InactivateCustomer(customerId);

            if(!result)
                return NotFound(new { message = "Customer not found" });

            return Ok(new { message = "Customer inactivated succesfully" });
        }

    
    }
}