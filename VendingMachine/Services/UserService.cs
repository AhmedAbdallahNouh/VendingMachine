using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using VendingMachine.Models;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Mapping;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.DTOs;
using VendingMachine.Interfaces;

namespace VendingMachine.Services
{
    public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<List<IAppUserDTO>> GetAll()
		{
			List<AppUser> users = await _userManager.Users.ToListAsync();

			return users.Count != 0 ? UserMapper.MapToUserDTOs(users) : new List<IAppUserDTO>();
		}

		public async Task<List<IAppUserDTO>> GetAllByRole(string roleName)
		{
			List<AppUser> users = (List<AppUser>)await _userManager.GetUsersInRoleAsync(roleName);

			return users.Count != 0 ? UserMapper.MapToUserDTOs(users) : new List<IAppUserDTO>();
		}

		public async Task<IAppUserDTO?> GetById(string userId)
		{
			AppUser? user = await _userManager.FindByIdAsync(userId);

			return user is not null ? UserMapper.MapToUserDTO(user) : null;
		}
		public async Task<IAppUserDTO?> GetByName(string userName)
		{
			AppUser? user = await _userManager.FindByNameAsync(userName);

			return user is not null ? UserMapper.MapToUserDTO(user) : null;
		}
		public async Task<IAppUserDTO?> Registrater(RegistrationUserDTO registrationUserDTO)
		{
			if (registrationUserDTO is not null)
			{
				AppUser user = UserMapper.MapToRegistrationUser(registrationUserDTO)!;

				IdentityResult addUserResult = await _userManager.CreateAsync(user, registrationUserDTO.Password);
				IdentityResult addRoleResult = await _roleManager.CreateAsync(new IdentityRole("Buyer"));
				IdentityResult addRoleToUserResult = await _userManager.AddToRoleAsync(user, registrationUserDTO.Role);
				if (addUserResult != null)
				{
					var userDTO = 	UserMapper.MapToUserDTO(user, registrationUserDTO.Role)!;
					userDTO.Role = registrationUserDTO.Role;
					return userDTO;
				}
				return null;
			}
			return null;
		}

		public async Task<IdentityResult> UpdateUserAsync(string userId, AppUserDTO userDTO, string newPassword)
		{

			if (userDTO is null || userDTO.Id != userId)
				return IdentityResult.Failed(new IdentityError { Description = "User not found Or you are not authorized." });

			AppUser? user = await _userManager.FindByIdAsync(userDTO.Id);

			if (user == null)
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });

			user = UserMapper.MapToUser(userDTO);
			if (!string.IsNullOrEmpty(newPassword))
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

				if (!result.Succeeded)
					return result;
			}

			return await _userManager.UpdateAsync(user!);
		}
		public async Task<IdentityResult> DeleteUserAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
			{
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });
			}

			return await _userManager.DeleteAsync(user);
		}
	
		//public async Task<ActionResult> Login(LoginDTO loginDTO)
		//{
		//	AppUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
		//	if (user != null)
		//	{
		//		bool valid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
		//		if (valid)
		//		{
		//			//Generate Token
		//			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456"));
		//			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
		//			expires: DateTime.Now.AddMinutes(120),
		//			signingCredentials: credentials,
		//			claims: claims);

		//			return Ok(new
		//			{
		//				token = new JwtSecurityTokenHandler().WriteToken(myToken),
		//				expiration = myToken.ValidTo
		//				//roles = userRoles
		//			});


		//		}
		//		else return Unauthorized();
		//	}
		//	return Unauthorized();

		//}

		//public async Task<ActionResult> addRoleToUser(RoleToUserDTO roleToUserDTO)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		foreach (var userId in roleToUserDTO.UserIds)
		//		{
		//			AppUser user = await _userManager.FindByIdAsync(userId);
		//			IdentityResult result = await _userManager.AddToRolesAsync(user, roleToUserDTO.RoleNames);
		//			if (result.Succeeded == false)
		//			{
		//				foreach (var error in result.Errors)
		//				{
		//					ModelState.AddModelError("", error.Description);
		//				}
		//				return BadRequest();
		//			}
		//		}
		//		return Ok(roleToUserDTO);
		//	}
		//	return BadRequest();
		//}

	}
}
