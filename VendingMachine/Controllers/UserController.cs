using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Interfaces;
using VendingMachine.Mapping;
using VendingMachine.Models;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			List<AppUserDTO> appUserDTOs =  await _userService.GetAll();

			if (!appUserDTOs.Any())
				return NotFound();

			return Ok(appUserDTOs);
		}
		[HttpGet("GetAll/{role:alpha}")]
		public async Task<IActionResult> GetAllByRole(string roleName)
		{
			List<AppUserDTO> appUserDTOs = await _userService.GetAllByRole(roleName);

			if (!appUserDTOs.Any())
				return NotFound();

			return Ok(appUserDTOs);
		}

		[HttpGet("{id:alpha}")]
		public async Task<IActionResult> GetById(string userId)
		{
			AppUserDTO? appUserDTO = await _userService.GetById(userId);

			if (appUserDTO == null)
				return NotFound();

			return Ok(appUserDTO);
		}
		[HttpGet("GetByUserName/{userName:alpha}")]
		public async Task<IActionResult> GetByUserName(string userName)
		{
			AppUserDTO? appUserDTO = await _userService.GetByName(userName);

			if (appUserDTO == null)
				return NotFound();

			return Ok(appUserDTO);
		}

		[HttpPost]
		public async Task<ActionResult> Registrater(RegistrationUserDTO registrationUserDTO)
		{
			if (ModelState.IsValid)
			{				
				AppUserDTO? userDTO = await _userService.Registrater(registrationUserDTO);				
				
				if(userDTO == null)
					return UnprocessableEntity("Unable to process the request to add the user.");

				return Ok(userDTO);
			}
			return BadRequest();
		}

		[HttpPut]
		public async Task<ActionResult> Update(string userId,AppUserDTO userDTO, string newPassword)
		{
			if (ModelState.IsValid)
			{
				IdentityResult result = await _userService.UpdateUserAsync(userId, userDTO ,newPassword);

				if (!result.Succeeded)
					return UnprocessableEntity("Unable to process the request to add the user.");

				return NoContent();
			}
			return BadRequest();
		}

		[HttpDelete]
		public async Task<ActionResult> Delete(string userId)
		{
			if (ModelState.IsValid)
			{
				IdentityResult result = await _userService.DeleteUserAsync(userId);

				if (!result.Succeeded)
					return UnprocessableEntity("Unable to process the request to add the user.");

				return NoContent();
			}
			return BadRequest();
		}
	}
}
