using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Services
{
	public class ProductServices
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductServices(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Product> GetAll() => _unitOfWork.Product.GetAll();
		public async Task<IEnumerable<Product>> GetAllAsync() => await _unitOfWork.Product.GetAllAsync();
		public IEnumerable<Product>? GetAllForSpecificSeller(string sellerId) => _unitOfWork.Product.FindAll(p => p.SellerId == sellerId);
		public IEnumerable<Product>? GetAllInStock() => _unitOfWork.Product.FindAll(p=> p.AmountAvailable > 0);
		public IEnumerable<Product>? GetAllInStockForSpecificUser(string sellerId) => _unitOfWork.Product.FindAll(p=> p.SellerId == sellerId && p.AmountAvailable > 0);
		public Product? GetById(int productId) => _unitOfWork.Product.Find(p => p.Id == productId);
		public Product? GetByName(string name) => _unitOfWork.Product.Find(p => p.Name == name);
		public Product? GetByNameForSpeicficSeller(string sellerId, string name) => _unitOfWork.Product.Find(p =>p.SellerId == sellerId &&  p.Name == name);
		public Product? GetBySellerId(string sellerId) => _unitOfWork.Product.Find(p => p.SellerId == sellerId);
		public Product? Add(ProductDTO) => _unitOfWork.Product.Add(p => p.SellerId == sellerId);
		public Product? Update(string sellerId) => _unitOfWork.Product.Find(p => p.SellerId == sellerId);
		public Product? Delete(string sellerId) => _unitOfWork.Product.Find(p => p.SellerId == sellerId);



    }
}
