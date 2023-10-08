using System.ComponentModel.DataAnnotations;

namespace ACBAbankTask.DataModels
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
