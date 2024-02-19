using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Filters;
using VendingMachine.Interfaces;


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
		[Authorize(AuthenticationSchemes = "Bearer" , Roles ="Seller")]

		public async Task<IActionResult> GetAll() 
		{
			List<IAppUserDTO> appUserDTOs =  await _userService.GetAll();

			if (!appUserDTOs.Any())
				return NotFound();

			return Ok(appUserDTOs);
		}

		[HttpGet("GetAll/{roleName:alpha}")]
		public async Task<IActionResult> GetAllByRole(string roleName)
		{
			List<IAppUserDTO> appUserDTOs = await _userService.GetAllByRole(roleName);

			if (!appUserDTOs.Any())
				return NotFound();

			return Ok(appUserDTOs);
		}

		[HttpGet("{userId:alpha}")]
		public async Task<IActionResult> GetById(string userId)
		{
			IAppUserDTO? appUserDTO = await _userService.GetById(userId);

			if (appUserDTO == null)
				return NotFound();

			return Ok(appUserDTO);
		}

		[HttpGet("GetByUserName/{userName:alpha}")]
		public async Task<IActionResult> GetByUserName(string userName)
		{
			IAppUserDTO? appUserDTO = await _userService.GetByName(userName);

			if (appUserDTO == null)
				return NotFound();

			return Ok(appUserDTO);
		}

		[HttpPost]
		public async Task<ActionResult> Add(RegistrationUserDTO registrationUserDTO)
		{
			if (ModelState.IsValid)
			{				
				IAppUserDTO? userDTO = await _userService.Registrater(registrationUserDTO);				
				
				if(userDTO == null)
					return UnprocessableEntity("Unable to process the request to add the user.");

				return Ok(userDTO);
			}
			return BadRequest();
		}

	    [HttpPost("{userId:alpha}/{newPassword:alpha}")]
		[TypeFilter(typeof(UserAuthorizationFilter))]
			public async Task<ActionResult> Update([FromRoute] string userId,[FromBody] AppUserDTO userDTO,[FromRoute] string newPassword)
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
		[TypeFilter(typeof(UserAuthorizationFilter))]
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
