using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Models;

namespace VendingMachine.DTOs
{
    public class ProductDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Product name is required")]
		public string Name { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Cost must be greater than or equal to 0")]
		public int AmountAvailable { get; set; }

		[Required(ErrorMessage = "Seller ID is required")]
		public decimal Cost { get; set; }

		public string SellerId { get; set; }
		public AppUserDTO? Seller { get; set; }
		public ICollection<BuyerProductDTO> BuyerProducts { get; set; }
	}
}
