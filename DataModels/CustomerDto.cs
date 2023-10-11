using System.ComponentModel.DataAnnotations;
using ACBAbankTask.Helpers;

namespace ACBAbankTask.Models
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Անունը պարտադիր լրացման ենթակա դաշտ է")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Անունը պարունակում է անվավեր սիմվոլներ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Սա պարտադիր լրացման ենթակա դաշտ է")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Անվավեր սիմվոլների ճանաչում")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "էլ․ փոստը պարտադիր լրացման դաշտ է")]
        [EmailAddress(ErrorMessage = "Ներմուծված արժեքը սխալ է")]
        [UniqueValidation("Email", "Email", ErrorMessage = "Ներմուծված էլ․ փոստը արդեն առկա է տվյալների բազայում")]
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
