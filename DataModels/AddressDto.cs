using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACBAbankTask.DataModels
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Խնդրում ենք լրացնել դաշտը")]
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        [Required(ErrorMessage = "Բնակության քաղաքը պարտադիր դաշտ է")]
        public string City { get; set; }
        [Required(ErrorMessage = "Բնակության երկիրը պարտադիր դաշտ է")]
        public string Country { get; set; }

    }
}
