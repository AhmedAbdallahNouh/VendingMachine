using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AmountAvailable { get; set; }
		public decimal Cost { get; set; }

		[ForeignKey(nameof(Seller))]
		public string SellerId { get; set; }
        public AppUser? Seller { get; set; }
		public ICollection<BuyerProduct> BuyerProducts { get; set; }

	}
}
