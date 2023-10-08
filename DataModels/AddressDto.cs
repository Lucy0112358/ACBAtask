using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACBAbankTask.DataModels
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Customer ID is required")]

        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
