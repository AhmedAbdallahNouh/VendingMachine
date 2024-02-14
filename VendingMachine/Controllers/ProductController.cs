using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Consts;
using VendingMachine.Interfaces;

namespace VendingMachine.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IBaseRepository<ProductController> _productRepository;

		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult GetById()
		{
			return Ok(_unitOfWork.Product.GetById(1));
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			return Ok(_unitOfWork.Product.GetAll());
		}

		[HttpGet("GetByName")]
		public IActionResult GetByName()
		{
			return Ok(_unitOfWork.Books.Find(b => b.Title == "New Book", new[] { "Author" }));
		}

		[HttpGet("GetAllWithAuthors")]
		public IActionResult GetAllWithAuthors()
		{
			return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("New Book"), new[] { "Author" }));
		}

		[HttpGet("GetOrdered")]
		public IActionResult GetOrdered()
		{
			return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
		}

		[HttpPost("AddOne")]
		public IActionResult AddOne()
		{
			var book = _unitOfWork.Books.Add(new Book { Title = "Test 4", AuthorId = 1 });
			_unitOfWork.Complete();
			return Ok(book);
		}
	}
}
