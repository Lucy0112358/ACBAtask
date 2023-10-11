using System.ComponentModel.DataAnnotations;

namespace ACBAbankTask.DataModels
{
    public class UserDto
    {
        [Required(ErrorMessage = "Էլ․ փոստը պարտադիր է")]
        [EmailAddress(ErrorMessage = "Անվավեր էլ․ փոստ")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
