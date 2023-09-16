using System.ComponentModel.DataAnnotations;
using ACBAbankTask.Helpers;

namespace ACBAbankTask.Models
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid characters in the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid characters in the name")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\+374[0-9]{8}$", ErrorMessage = "Please enter Armenian mobile number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Passport number is required")]
        [RegularExpression(@"^P\d{7}$", ErrorMessage = "Invalid passport number")]
        [UniqueValidation("Passport", "Passport")]
        public string Passport { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        public string IssuedBy { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [UniqueValidation("Email", "Email")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssuedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CratedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }
}
