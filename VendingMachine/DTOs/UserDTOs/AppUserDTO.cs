using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using VendingMachine.Validations;

namespace VendingMachine.DTOs.UserDTOs
{
    public class AppUserDTO : IBaseEntity, IAppUserDTO
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
	}
}
