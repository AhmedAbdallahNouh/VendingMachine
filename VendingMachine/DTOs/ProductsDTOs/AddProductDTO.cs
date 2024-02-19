using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.DTOs.ProductsDTOs
{
	public class AddProductDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AmountAvailable { get; set; }
		public decimal Cost { get; set; }
		public string SellerId { get; set; }
	}
}
