using System.ComponentModel.DataAnnotations;
using VendingMachine.Interfaces;

namespace VendingMachine.DTOs.UserDTOs
{
	public class SellerDTO : IAppUserDTO
	{
	
		public ICollection<ProductDTO> SellerProductsDTOs { get; set; } = new List<ProductDTO>();
		public string Id { get; set; }
		public string Role { get; set; }
		public string UserName { get; set; }
	}
}
