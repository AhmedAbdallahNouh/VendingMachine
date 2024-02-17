using Microsoft.AspNetCore.Identity;
using VendingMachine.DTOs.UserDTOs;

namespace VendingMachine.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<List<IAppUserDTO>> GetAll();
        Task<List<IAppUserDTO>> GetAllByRole(string roleName);
        Task<IAppUserDTO?> GetById(string userId);
        Task<IAppUserDTO?> GetByName(string userName);
        Task<IAppUserDTO?> Registrater(RegistrationUserDTO registrationUserDTO);
        Task<IdentityResult> UpdateUserAsync(string userId, AppUserDTO userDTO, string newPassword);
    }
}