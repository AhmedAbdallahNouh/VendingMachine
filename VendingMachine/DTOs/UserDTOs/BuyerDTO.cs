using System.ComponentModel.DataAnnotations;
using VendingMachine.Interfaces;
using VendingMachine.Validations;

namespace VendingMachine.DTOs.UserDTOs
{
	public class BuyerDTO : IAppUserDTO
	{
		[DepositAcceptedValue(ErrorMessage = "Deposit must be 5, 10, 20, 50, or 100")]
		public int? Deposit { get; set; }
		public ICollection<BuyerProductDTO> BuyerProductDTOs { get; set; } = new List<BuyerProductDTO>();
		public string Id { get ; set; }
		public string Role { get ; set ; }
		public string UserName { get; set; }
	}
}
