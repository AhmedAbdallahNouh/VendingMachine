using System.ComponentModel.DataAnnotations;
using VendingMachine.Validations;

namespace VendingMachine.DTOs.UserDTOs
{
    public class BaseUserDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(20)]
        public string UserName { get; set; }

        [DepositAcceptedValue(ErrorMessage = "Deposit must be 5, 10, 20, 50, or 100")]
        public int? Deposit { get; set; }
    }
}
