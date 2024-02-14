using Microsoft.AspNetCore.Identity;
using VendingMachine.Models;

namespace VendingMachine.DTOs
{
	public class AppUserDTO
	{
        public string Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public decimal Deposit { get; set; }
		public ICollection<ProductDTO> SellerProductsDTOs { get; set; }  = new List<ProductDTO>();
		//public ICollection<IdentityUserRole<string>> UserRoles { get; }
		public ICollection<BuyerProductDTO> BuyerProducts { get; set; } = new List<BuyerProductDTO>();

	}
}
