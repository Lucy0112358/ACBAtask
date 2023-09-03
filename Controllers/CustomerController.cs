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
                return StatusCode(500, "An error occurred while creating the customer.");
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
