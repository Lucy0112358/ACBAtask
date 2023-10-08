using ACBAbankTask.DataModels;
using ACBAbankTask.Models;
using ACBAbankTask.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACBAbankTask.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {        
            _customerService = customerService;
        }


        [HttpGet("search")]
        public IActionResult SearchCustomers([FromQuery] string name, [FromQuery] string surname, [FromQuery] string email, [FromQuery] string mobile, [FromQuery] string document, [FromQuery] int pageNumber)
        {
            List<object> customers = _customerService.SearchCustomers(name, surname, email, mobile, pageNumber, document);

            return Ok(customers);
        }

        // POST api/<CustomerController>
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(RequestDto request)
        {
            if (request.customer == null)
            {
                return BadRequest("Customer data is invalid.");
            }
            try
            {
                int customerId = await _customerService.CreateCustomer(request.customer, request.documents, request.address, request.mobile);
                return Ok(request.customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, [FromBody] CustomerDto updatedCustomer)
        {
            try
            {
                var success = await _customerService.EditCustomerAsync(id, updatedCustomer);             
                    return Ok("Customer updated successfully.");              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var isDeleted = await _customerService.DeleteCustomerAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
