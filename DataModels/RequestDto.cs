using ACBAbankTask.Entities;
using ACBAbankTask.Models;

namespace ACBAbankTask.DataModels
{
    public class RequestDto
    {
        public CustomerDto customer { get; set; }
        public List<DocumentDto> documents { get; set; }
        public List<AddressDto> address { get; set; }
        public List<MobileDto> mobile { get; set; }
    }
}
