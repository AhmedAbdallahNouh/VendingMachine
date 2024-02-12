using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Models
{
	public class BuyerProduct
	{
		public int ProductId { get; set; }

		[ForeignKey(nameof(Buyer))]
		public string BuyerId { get; set; }
		public AppUser? Buyer { get; set; }

		[ForeignKey(nameof(Product))]
		public Product Product { get; set; }
		public int Quantity { get; set; } 
	}
}

