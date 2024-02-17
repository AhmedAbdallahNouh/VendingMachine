using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DTOs.UserDTOs
{
    public class RegistrationUserDTO
    {
		[Required(ErrorMessage = "Username is required")]
		[StringLength(20)]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
