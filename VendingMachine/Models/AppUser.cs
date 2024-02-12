using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models
{
	public class AppUser: IdentityUser
	{
		public decimal Deposit { get; set; }
		public ICollection<Product> SellerProducts { get; set; } 
		public ICollection<IdentityUserRole<string>> UserRoles { get; }
		public ICollection<BuyerProduct> BuyerProducts { get; set; }
	}
}
