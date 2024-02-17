using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Models
{
	public class BuyerProduct
	{
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }

		[ForeignKey(nameof(Buyer))]
		public string BuyerId { get; set; }
		public int Quantity { get; set; }
		
		public AppUser? Buyer { get; set; }
		public Product Product { get; set; }
	}
}

