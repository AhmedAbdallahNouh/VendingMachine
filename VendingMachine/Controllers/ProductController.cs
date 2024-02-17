using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Consts;
using VendingMachine.DTOs;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductServices _productServices;

		public ProductController(IProductServices productServices)
		{
			_productServices = productServices;
		}


		[HttpGet]
		//[Route("api/products")]
		public IActionResult GetAll()
		{
			List<ProductDTO> productDTOs = _productServices.GetAll().ToList();
			
			if (productDTOs.Count == 0) 
				return NotFound();

			return Ok(productDTOs);
		}

		[HttpGet("GetAllForSeller")]
		//[Route("api/products/seller")]
		public async Task<IActionResult> GetAllGetAllAsync()
		{
			var productDTOs = await _productServices.GetAllAsync();

			if (productDTOs.Count() == 0)
				return NotFound();

			return Ok(productDTOs.ToList());
		}

		[HttpGet("GetAllAvailable")]
		//[Route("api/products/available")]
		public IActionResult GetAllAvailable()
		{
			List<ProductDTO> productDTOs = _productServices.GetAllAvailable().ToList();

			if (productDTOs.Count == 0)
				return NotFound();

			return Ok(productDTOs);
		}

		[HttpGet("GetAllNotAvailable")]
		//[Route("api/products/notavailable")]
		public IActionResult GetAllNotAvaliable()
		{
			List<ProductDTO> productDTOs = _productServices.GetAllNotAvaliable().ToList();

			if (productDTOs.Count == 0)
				return NotFound();

			return Ok(productDTOs);
		}

		[HttpGet("GetAllAvailableForSpecificSeller/{id:alpha}")]
		//[Route("api/products/seller/available")]
		public IActionResult GetAllAvailableForSpecificUser(string sellerId)
		{
			List<ProductDTO> productDTOs = _productServices.GetAllForSpecificSeller(sellerId).ToList();

			if (productDTOs.Count == 0)
				return NotFound();

			return Ok(productDTOs);
		}

		[HttpGet("GetAllNotAvailableForSpecificSeller/{id:alpha}")]
		//[Route("api/products/seller/notavailable")]
		public IActionResult GetAllNotAvaliableForSpecificUser(string sellerId)
		{
			List<ProductDTO> productDTOs = _productServices.GetAllNotAvaliableForSpecificUser(sellerId).ToList();

			if (productDTOs.Count == 0)
				return NotFound();

			return Ok(productDTOs);
		}

		[HttpGet("GetById/{id :int}")]
		//[Route("api/product/id")]
		public IActionResult GetById(int productId)
		{
			ProductDTO? productDTO = _productServices.GetById(productId);

			if (productDTO is null)
				return NotFound();

			return Ok(productDTO);
		}
		
		[HttpGet("GetByIdAscync/{id:int}")]
		//[Route("api/product/task")]
		public async Task<IActionResult> GetByIdAsync(int productId)
		{
			ProductDTO? productDTO = await _productServices.GetByIdAsync(productId);

			if (productDTO is null)
				return NotFound();

			return Ok(productDTO);
		}


		[HttpGet("GetByName/{name:alpha}")]
		public IActionResult GetByName(string productName)
		{
			ProductDTO? productDTO =  _productServices.GetByName(productName);

			if (productDTO is null)
				return NotFound();

			return Ok(productDTO);
		}

		[HttpGet("GetByNameAsync/{name:alpha}")]
		public async Task<IActionResult> GetByNameAsync(string productName)
		{
			ProductDTO? productDTO = await _productServices.GetByNameAsync(productName);

			if (productDTO is null)
				return NotFound();

			return Ok(productDTO);
		}
		
		[HttpPost("Add")]
		public IActionResult Add([FromBody] ProductDTO productDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			ProductDTO? addedProduct = _productServices.Add(productDTO);
			
			if (addedProduct is null)
				return UnprocessableEntity("Unable to process the request to add the product.");

			return Ok(addedProduct);
		}

		[HttpPut("Edit/{id:alpha}")]
		public IActionResult Update(string sellerId,[FromBody] ProductDTO productDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			int numberOfRowsAffected = _productServices.Update(sellerId,productDTO);

			if (numberOfRowsAffected <= 0)
				return UnprocessableEntity("Unable to process the request to add the product.");

			return NoContent();
		}
		[HttpPut("Delete/{id:alpha}/{sellerId:alpha}")]
		public IActionResult Delete(string sellerId, int productId)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			int numberOfRowsAffected = _productServices.Delete(sellerId, productId);

			if (numberOfRowsAffected <= 0)
				return UnprocessableEntity("Unable to process the request to add the product.");

			return NoContent();
		}
	}
}
