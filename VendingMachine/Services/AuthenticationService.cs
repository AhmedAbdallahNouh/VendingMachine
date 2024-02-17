using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendingMachine.DTOs.AuthenticationDTOs;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<AppUser> _userManager;

		public AuthenticationService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<LoginResultDTO> Login(LoginDTO loginDTO)
		{
			AppUser? user = await _userManager.FindByNameAsync(loginDTO.UserName);
			if (user is not null)
			{
				bool valid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
				if (valid)
				{
					//Generate Token
					var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456"));
					var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

					var claims = new List<Claim>();
					claims.Add(new Claim(ClaimTypes.Name, user.UserName));
					claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
					claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


					//Get Role
					var userRoles = await _userManager.GetRolesAsync(user);
					foreach (var userRole in userRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, userRole));
					}


					var myToken = new JwtSecurityToken(
					expires: DateTime.Now.AddMinutes(120),
					signingCredentials: credentials,
					claims: claims);

					return new LoginResultDTO()
					{
						Success = true,
						Token = new JwtSecurityTokenHandler().WriteToken(myToken),
						Expiration = myToken.ValidTo
					};


				}
				return new LoginResultDTO()
				{
					Success = false,
					ErrorMessage = "Invalid credentials"
				};
			}
			return new LoginResultDTO()
			{
				Success = false,
				ErrorMessage = "Invalid credentials"
			};

		}
	}
}
