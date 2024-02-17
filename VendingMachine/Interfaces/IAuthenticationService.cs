using VendingMachine.DTOs.AuthenticationDTOs;

namespace VendingMachine.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResultDTO> Login(LoginDTO loginDTO);
    }
}