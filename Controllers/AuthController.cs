using System.ComponentModel.DataAnnotations;
using ACBAbankTask.DataModels;
using ACBAbankTask.Helpers.Validations;
using ACBAbankTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACBAbankTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly UserValidation _validUser;

        public AuthController(IConfiguration configuration, IUserService userService, UserValidation validUser)
        {
            _configuration = configuration;
            _userService = userService;
            _validUser = validUser;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto user)
        {
            if (!_validUser.IsEmailUnique(user.Email))
            {
                return BadRequest("Ներմուծված էլ․ փոստը արդեն առկա է տվյալների բազայում");
            }
            var success = _userService.Register(user);
            return Ok(success);
        }


        [HttpGet("signin")]
        public IActionResult SignIn([FromQuery] string email, [FromQuery] string password)
        {
            var success = _userService.SignIn(email, password);
            return Ok(success);
        }
    }
}
