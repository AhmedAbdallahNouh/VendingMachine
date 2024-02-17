namespace VendingMachine.DTOs.AuthenticationDTOs
{
	public class LoginResultDTO
	{
		public bool Success { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
		public string Token { get; set; }
		public DateTime? Expiration { get; set; }
	}
}
