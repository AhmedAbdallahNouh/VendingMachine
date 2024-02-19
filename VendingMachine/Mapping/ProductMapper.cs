using VendingMachine.DTOs;
using VendingMachine.DTOs.ProductsDTOs;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Models;

namespace VendingMachine.Mapping
{
	public static class ProductMapper
	{
	
		public static ProductDTO? MapToProductDTO(Product product)
		{
			if(product is not null)
			{
				ProductDTO ProductDTO = new ProductDTO()
				{
					Id = product.Id,
					Name = product.Name,
					AmountAvailable = product.AmountAvailable,
					Cost = product.Cost,
					SellerId = product.SellerId,
					SellerDTO = (SellerDTO)(product.Seller is not null ? UserMapper.MapToUserDTO(product.Seller, "Seller") : null),

				};
				return ProductDTO;
			}
			return null;
			
		}
		public static ProductDTO? MapToAddProductDTO(AddProductDTO addProductDTO)
		{
			if (addProductDTO is not null)
			{
				ProductDTO ProductDTO = new ProductDTO()
				{
					Id = addProductDTO.Id,
					Name = addProductDTO.Name,
					AmountAvailable = addProductDTO.AmountAvailable,
					Cost = addProductDTO.Cost,
					SellerId = addProductDTO.SellerId,
				};
				return ProductDTO;
			}
			return null;

		}
		public static Product? MapToProduct(ProductDTO productDTO)
		{
			if (productDTO is not null)
			{
				Product Product = new Product()
				{
					Id = productDTO.Id,
					Name = productDTO.Name,
					AmountAvailable = productDTO.AmountAvailable,
					Cost = productDTO.Cost,
					SellerId = productDTO.SellerId,
					//Seller = productDTO.SellerDTO is not null ? UserMapper.MapToUser(productDTO.SellerDTO) : null,
					//KeyDTOs = Product.Keys.Select(k => MapKeyToDto(k)).ToList()
				};
				if (productDTO.SellerDTO is not null)
					Product.Seller = UserMapper.MapToUser(productDTO.SellerDTO);
				return Product;
			}
			return null;
			
		}
		public static List<ProductDTO> MapToProductDTOs(List<Product> products)
		{
			if (products.Any())
			{
				List<ProductDTO> productDTOs = products.Select(product => MapToProductDTO(product)).ToList();
				return productDTOs;
			}
			return new List<ProductDTO>();
			
		}
		public static List<Product> MapToProducts(List<ProductDTO> productDTOs)
		{
			if (productDTOs.Any())
			{
				List<Product> products = productDTOs.Select(productDTO => MapToProduct(productDTO)).ToList();
				return products;
			}
			return new List<Product>();
			
		}
	}
}
