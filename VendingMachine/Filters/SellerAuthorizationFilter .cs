using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using VendingMachine.Interfaces;

namespace VendingMachine.Filters
{
	public class SellerAuthorizationFilter : IAuthorizationFilter
	{
		private readonly IProductService _productService;

		public SellerAuthorizationFilter(IProductService productService)
		{
			_productService = productService;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var idValue = context.RouteData.Values["id"]?.ToString();

			if (!int.TryParse(idValue, out int productId))
			{
				context.Result = new BadRequestResult();
				return;
			}
			// Get the current user's ID from the token
			string currentUserId = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			// Check if the current user is the seller of the product
			bool isSellerAuthorized = _productService.IsSellerAuthorized(currentUserId, productId);

			if (!isSellerAuthorized)
			{
				context.Result = new ForbidResult();
			}
		}
	
	}
}
