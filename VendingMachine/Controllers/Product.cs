using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Interfaces;

namespace VendingMachine.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Product : ControllerBase
	{
		private readonly IBaseRepository<Product> _productRepository;

		public Product(IBaseRepository<Product> productRepository)
        {
			_productRepository = productRepository;
		}
		[HttpGet]
		public ActionResult GetAll()
		{
			 _productRepository.GetAll();
		}
    }
}
