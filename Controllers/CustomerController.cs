using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ACBAbankTask.Entities;
using ACBAbankTask.Models;
using ACBAbankTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("signin")]
        public IActionResult SignIn(string email, string password)
        {
            // Create a SqlConnection using the connection string from appsettings.json
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Create a SqlCommand to execute your SQL query
                using (var command = new SqlCommand("SELECT Id, Email, Password FROM Customers WHERE Email = @Email AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string userEmail = reader.GetString(1);
                            return Ok(GetAccessToken(userEmail, userId));
                        }
                        else
                        {
                            return Unauthorized("Invalid email or password");
                        }
                    }
                }
            }
        }

        private string GetAccessToken(string email, int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("issuer",
              "aud",
              new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("role", "Admin")
              },
              expires: DateTime.UtcNow.AddDays(14),
              signingCredentials: credentials
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }


        /*        [HttpGet("signin")]
                public IActionResult SignIn(string email, string password)
                {
                    var users = new List<User>()
                {
                    new User
                    {
                        Id=1,
                        Email=email,
                        Password="pass",
                    }
                };
                    var user = users.FirstOrDefault(u => u.Email == email);
                    return Ok(GetAccessToken(user.Email, user.Id));
                }

                private string GetAccessToken(string email, int id)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken("issuer",
                      "aud",
                      new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.NameId,id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email,email),
                        new Claim("role", "Admin")
                      },
                      expires: DateTime.UtcNow.AddDays(14),
                      signingCredentials: credentials
                      );
                    var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return accessToken;
                }
        */

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("search")]
        public IActionResult SearchCustomers([FromQuery] string name, [FromQuery] string surname, [FromQuery] string email, [FromQuery] string mobile)
        {
            var customers = _customerService.SearchCustomers(name, surname, email, mobile);

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
