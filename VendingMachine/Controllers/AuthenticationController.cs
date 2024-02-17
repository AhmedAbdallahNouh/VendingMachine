using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendingMachine.DTOs.AuthenticationDTOs;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _authenticationService;
		public AuthenticationController(IAuthenticationService authenticationService)
        {
			_authenticationService = authenticationService;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			LoginResultDTO loginResult = await _authenticationService.Login(loginDTO);

			if (loginResult.Success)
			{
				return Ok(new
				{
					token = loginResult.Token,
					expiration = loginResult.Expiration
				});
			}

			return Unauthorized(new { error = loginResult.ErrorMessage });	
		}
	}
}
