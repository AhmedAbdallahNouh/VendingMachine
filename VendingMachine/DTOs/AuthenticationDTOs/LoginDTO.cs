using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DTOs.AuthenticationDTOs
{
	public class LoginDTO
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
