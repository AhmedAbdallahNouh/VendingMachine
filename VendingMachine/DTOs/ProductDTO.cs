using System.ComponentModel.DataAnnotations.Schema;
using VendingMachine.Models;

namespace VendingMachine.DTOs
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AmountAvailable { get; set; }
		public decimal Cost { get; set; }

		public string SellerId { get; set; }
		public AppUserDTO? Seller { get; set; }
		public ICollection<BuyerProductDTO> BuyerProducts { get; set; }
	}
}
