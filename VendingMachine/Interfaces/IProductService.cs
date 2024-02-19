using VendingMachine.DTOs;
using VendingMachine.Models;

namespace VendingMachine.Interfaces
{
    public interface IProductService
    {
        ProductDTO? Add(ProductDTO productDTO);
        int Delete(string sellerId, int productDtoId);
        IEnumerable<ProductDTO> GetAll();
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        IEnumerable<ProductDTO> GetAllAvailable();
        IEnumerable<ProductDTO> GetAllAvailableForSpecificUser(string sellerId);
        IEnumerable<ProductDTO> GetAllForSpecificSeller(string sellerId);
        IEnumerable<ProductDTO> GetAllNotAvaliable();
        IEnumerable<ProductDTO> GetAllNotAvaliableForSpecificUser(string sellerId);
        ProductDTO? GetById(int productId);
        Task<ProductDTO?> GetByIdAsync(int productId);
        ProductDTO? GetByName(string name);
        Task<ProductDTO?> GetByNameAsync(string name);
        ProductDTO? GetByNameForSpeicficSeller(string sellerId, string name);
        public bool IsSellerAuthorized(string sellerId, int productId);

		int Update(ProductDTO productDTO);
    }
}