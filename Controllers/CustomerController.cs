using ACBAbankTask.Entities;
using ACBAbankTask.Models;
using ACBAbankTask.Services;
using Microsoft.AspNetCore.Mvc;


namespace ACBAbankTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CustomerService _customerService;


        public CustomerController(IConfiguration configuration, CustomerService customerService)
        {
            _configuration = configuration;
               _customerService = customerService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("search")]
        public IActionResult SearchCustomers([FromQuery] string name, [FromQuery] string surname, [FromQuery] string mobile, [FromQuery] string passport)
        {
            var customers = _customerService.SearchCustomers(name, surname, mobile, passport);

            return Ok(customers);
        }
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customer)
        {
            if (customer == null)
            {

                return BadRequest("Customer data is invalid.");
            }
            try
            {
                int customerId = await _customerService.CreateCustomer(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed.
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

                if (success)
                {
                    return Ok("Customer updated successfully.");
                }
                else
                {
                    return NotFound("Customer not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
