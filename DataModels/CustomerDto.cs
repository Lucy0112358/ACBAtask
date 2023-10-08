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

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
 /*       [UniqueValidation("Email", "Email")]*/
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
    }
}
