using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Validations;

namespace VendingMachine.Models
{
	public class AppUser: IdentityUser
	{
		[DepositAcceptedValue(ErrorMessage = "Deposit must be 5, 10, 20, 50, or 100")]
		public int? Deposit { get; set; }
		public ICollection<Product> SellerProducts { get; set; } 
		public ICollection<IdentityUserRole<string>> UserRoles { get; }
		public ICollection<BuyerProduct> BuyerProducts { get; set; }
	}
}
