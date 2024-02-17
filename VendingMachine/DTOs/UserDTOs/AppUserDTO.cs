using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Models;
using VendingMachine.Validations;

namespace VendingMachine.DTOs.UserDTOs
{
    public class AppUserDTO : BaseUserDTO
    {
        public ICollection<ProductDTO> SellerProductsDTOs { get; set; } = new List<ProductDTO>();
        //public ICollection<IdentityUserRole<string>> UserRoles { get; }
        public ICollection<BuyerProductDTO> BuyerProducts { get; set; } = new List<BuyerProductDTO>();

    }
}
