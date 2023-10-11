using System.ComponentModel.DataAnnotations;

namespace ACBAbankTask.DataModels
{
    public class MobileDto
    {
        [Required(ErrorMessage = "Սա պարտադիր լրացման դաշտ է")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Սա պարտադիր լրացման դաշտ է")]
        public string Title { get; set; }

        [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Երկրի կոդը անվավեր է")]
        public string CountryCode { get; set; }

        [RegularExpression(@"^[1-9]\d{7}$", ErrorMessage = "Ներմուծված արժեքն անվավեր է")]
        public string Number { get; set; }
    }
}
