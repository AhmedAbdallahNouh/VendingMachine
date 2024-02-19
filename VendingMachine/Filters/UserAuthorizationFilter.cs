using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VendingMachine.Filters
{
	public class UserAuthorizationFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var routeUserId = context.RouteData.Values["id"]?.ToString();

			if (userId != routeUserId)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
