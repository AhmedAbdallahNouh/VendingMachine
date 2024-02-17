using Microsoft.AspNetCore.Identity;
using VendingMachine.DTOs.UserDTOs;

namespace VendingMachine.Services
{
	public interface IUserService
	{
		Task<IdentityResult> DeleteUserAsync(string userId);
		Task<List<AppUserDTO>> GetAll();
		Task<List<AppUserDTO>> GetAllByRole(string roleName);
		Task<AppUserDTO?> GetById(string userId);
		Task<AppUserDTO?> GetByName(string userName);
		Task<AppUserDTO?> Registrater(RegistrationUserDTO registrationUserDTO);
		Task<IdentityResult> UpdateUserAsync(string userId, AppUserDTO userDTO, string newPassword);
	}
}