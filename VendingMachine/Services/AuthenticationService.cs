using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VendingMachine.DTOs.AuthenticationDTOs;
using VendingMachine.Helpers;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly JWT _jwt;

		public AuthenticationService(UserManager<AppUser> userManager, IOptions<JWT> jwt)
		{
			_userManager = userManager;
			_jwt = jwt.Value;

		}

		//public async Task<LoginResultDTO> Login(LoginDTO loginDTO)
		//{
		//	AppUser? user = await _userManager.FindByNameAsync(loginDTO.UserName);
		//	if (user is not null)
		//	{
		//		bool valid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
		//		if (valid)
		//		{
						
		//			// Use the same key for validation as configured in program.cs
		//			var validationKey = Encoding.UTF8.GetBytes("my_secret_key_123456_vending_machine_#245565__");

		//			var securityKey = new SymmetricSecurityKey(validationKey);
		//			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		//			var userClaims = await _userManager.GetClaimsAsync(user);

		//			var claims = new List<Claim>();
		//			claims.Add(new Claim(ClaimTypes.Name, user.UserName));
		//			claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
		//			claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
					

		//			//Get Role
		//			var userRoles = await _userManager.GetRolesAsync(user);
		//			foreach (var userRole in userRoles)
		//			{
		//				claims.Add(new Claim(ClaimTypes.Role, userRole));
		//			}


		//			var myToken = new JwtSecurityToken(
		//			expires: DateTime.Now.AddMinutes(500),
		//			signingCredentials: credentials,
					
		//			claims: claims);

		//			return new LoginResultDTO()
		//			{
		//				Success = true,
		//				Token = new JwtSecurityTokenHandler().WriteToken(myToken),
		//				Expiration = myToken.ValidTo
		//			};


		//		}
		//		return new LoginResultDTO()
		//		{
		//			Success = false,
		//			ErrorMessage = "Invalid credentials"
		//		};
		//	}
		//	return new LoginResultDTO()
		//	{
		//		Success = false,
		//		ErrorMessage = "Invalid credentials"
		//	};

		//}

		//DecCreed
		public async Task<LoginResultDTO> Login(LoginDTO loginDTO)
		{
			AppUser? user = await _userManager.FindByNameAsync(loginDTO.UserName);
			if (user is not null)
			{
				bool valid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
				if (valid)
				{

					var userClaims = await _userManager.GetClaimsAsync(user);
					var roles = await _userManager.GetRolesAsync(user);
					var roleClaims = new List<Claim>();

					foreach (var role in roles)
						roleClaims.Add(new Claim("roles", role));

					//var claims = new[]
					//{
					//new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					//new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					//new Claim(ClaimTypes.NameIdentifier, user.Id),
					//new Claim("uid", user.Id)
					//}
					var claims = new List<Claim>();
					claims.Add(new Claim(ClaimTypes.Name, user.UserName));
					claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
					claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
					claims = claims.Union(userClaims).Union(roleClaims).ToList();
					

					var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
					var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

					var jwtSecurityToken = new JwtSecurityToken(
						issuer: _jwt.Issuer,
						audience: _jwt.Audience,
						claims: claims,
						expires: DateTime.Now.AddDays(_jwt.DurationInDays),
						signingCredentials: signingCredentials);

					return new LoginResultDTO()
					{
						Success = true,
						Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
						Expiration = jwtSecurityToken.ValidTo
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
