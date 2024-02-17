using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Validations;

namespace VendingMachine.Models
{
	public class AppUser: IdentityUser
	{
		public int? Deposit { get; set; } = 0;
		public ICollection<Product> SellerProducts { get; set; } 
		public ICollection<IdentityUserRole<string>> UserRoles { get; }
		public ICollection<BuyerProduct> BuyerProducts { get; set; }
	}
}
