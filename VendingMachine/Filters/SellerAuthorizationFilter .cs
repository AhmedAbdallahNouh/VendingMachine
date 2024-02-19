using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using VendingMachine.DTOs;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Filters
{
	public class SellerAuthorizationFilter : IActionFilter
	{
		private readonly IProductService _productService;

		public SellerAuthorizationFilter(IProductService productService)
		{
			_productService = productService;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			object? product;
			ProductDTO productDTO = null;
			if (context.ActionArguments.TryGetValue("productDTO", out product))
			{
				productDTO = (ProductDTO)product;
			}

			var parameter1 = context.ActionArguments["productDTO"];
			// Get the current user's ID from the token
			string currentUserId = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			//var parameter2 = context.ActionArguments["parameter2"];

			bool isSellerAuthorized = _productService.IsSellerAuthorized(currentUserId, productDTO.Id);
			if (productDTO is null)
			{
				context.Result = new BadRequestResult();
				return;
			}

		

			// Check if the current user is the seller of the product
			

			if (!isSellerAuthorized)
			{
				context.Result = new ForbidResult();
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			// Implementation for OnActionExecuted if needed
		}
	}
	//public class SellerAuthorizationFilter : IAuthorizationFilter
	//{
	//	private readonly IProductService _productService;

	//	public SellerAuthorizationFilter(IProductService productService)
	//	{
	//		_productService = productService;
	//	}

	//	public void OnAuthorization(AuthorizationFilterContext context)
	//	{

	//		var idValue = context.RouteData.Values["id"]?.ToString();

	//		if (!int.TryParse(idValue, out int productId))
	//		{
	//			context.Result = new BadRequestResult();
	//			return;
	//		}
	//		// Get the current user's ID from the token
	//		string currentUserId = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

	//		// Check if the current user is the seller of the product
	//		bool isSellerAuthorized = _productService.IsSellerAuthorized(currentUserId, productId);

	//		if (!isSellerAuthorized)
	//		{
	//			context.Result = new ForbidResult();
	//		}
	//	}

	//}
}
