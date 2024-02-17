using VendingMachine.DTOs;
using VendingMachine.Interfaces;
using VendingMachine.Mapping;
using VendingMachine.Models;
using VendingMachine.UnitOfWork;

namespace VendingMachine.Services
{
    public class ProductService : IProductServices
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<ProductDTO> GetAll() => ProductMapper.MapToProductDTOs( _unitOfWork.Product.GetAll().ToList());
		public async Task<IEnumerable<ProductDTO>> GetAllAsync()
		{
			IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync();
			return ProductMapper.MapToProductDTOs(products.ToList());
		}
		public IEnumerable<ProductDTO> GetAllForSpecificSeller(string sellerId)
		{
			IEnumerable<Product>? products = _unitOfWork.Product.FindAll(p => p.SellerId == sellerId);

			if (products!.Any())
				ProductMapper.MapToProductDTOs(products.ToList());

			return new List<ProductDTO>();
		}
		public IEnumerable<ProductDTO> GetAllAvailable()
		{
			IEnumerable<Product>? products = _unitOfWork.Product.FindAll(p => p.AmountAvailable > 0);

			if (products!.Any())
				ProductMapper.MapToProductDTOs(products.ToList());

			return new List<ProductDTO>();
		}

		public IEnumerable<ProductDTO> GetAllNotAvaliable()
		{
			IEnumerable<Product>? products = _unitOfWork.Product.FindAll(p => p.AmountAvailable == 0);

			if (products!.Any())
				ProductMapper.MapToProductDTOs(products.ToList());

			return new List<ProductDTO>();
		}
		public IEnumerable<ProductDTO> GetAllAvailableForSpecificUser(string sellerId)
		{
			IEnumerable<Product>? products = _unitOfWork.Product.FindAll(p => p.SellerId == sellerId && p.AmountAvailable > 0);

			if (products!.Any())
				ProductMapper.MapToProductDTOs(products.ToList());

			return new List<ProductDTO>();
		}
		public IEnumerable<ProductDTO> GetAllNotAvaliableForSpecificUser(string sellerId)
		{
			IEnumerable<Product>? products = _unitOfWork.Product.FindAll(p => p.SellerId == sellerId && p.AmountAvailable == 0);

			if (products!.Any())
				ProductMapper.MapToProductDTOs(products.ToList());

			return new List<ProductDTO>();
		}
		public ProductDTO? GetById(int productId)
		{
			Product? product = _unitOfWork.Product.Find(p => p.Id == productId);

			if (product is not null)
				ProductMapper.MapToProductDTO(product);

			return null;
		}
		public async Task<ProductDTO?> GetByIdAsync(int productId)
		{
			Product? product = await _unitOfWork.Product.FindAsync(p => p.Id == productId);

			if (product is not null)
				ProductMapper.MapToProductDTO(product);

			return null;
		}
		public ProductDTO? GetByName(string name)
		{
			Product? product = _unitOfWork.Product.Find(p => p.Name == name);

			if (product is not null)
				ProductMapper.MapToProductDTO(product);
			return null;
		}
		public async Task<ProductDTO?> GetByNameAsync(string name)
		{
			Product? product = await _unitOfWork.Product.FindAsync(p => p.Name == name);

			if (product is not null)
				ProductMapper.MapToProductDTO(product);
			return null;
		}
		public ProductDTO? GetByNameForSpeicficSeller(string sellerId, string name)
		{
			Product? product = _unitOfWork.Product.Find(p => p.SellerId == sellerId && p.Name == name);

			if (product is not null)
				ProductMapper.MapToProductDTO(product);

			return null;
		}
		public ProductDTO? Add(ProductDTO productDTO)
		{
			if (productDTO == null)
				return null;

			Product? product = _unitOfWork.Product.Add(ProductMapper.MapToProduct(productDTO)!);

			return product is not null ? ProductMapper.MapToProductDTO(product) : null;
		}

		public int Update(string sellerId, ProductDTO productDTO)
		{
			if (productDTO == null || productDTO.SellerId != sellerId)
				return -1;

			_unitOfWork.Product.Attach(ProductMapper.MapToProduct(productDTO)!);
			var numberOfRowsAffected = _unitOfWork.Complete();

			return numberOfRowsAffected;
		}
		public int Delete(string sellerId, int productDtoId)
		{
			ProductDTO? productDTO = GetById(productDtoId);

			if (productDTO == null || productDTO.SellerId != sellerId)
				return -1;

			_unitOfWork.Product.Delete(ProductMapper.MapToProduct(productDTO)!);
			var numberOfRowsAffected = _unitOfWork.Complete();

			return numberOfRowsAffected;
		}

		
	}
}
