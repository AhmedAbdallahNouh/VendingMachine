using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DTOs.UserDTOs
{
    public class RegistrationUserDTO : BaseUserDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
    }
}
