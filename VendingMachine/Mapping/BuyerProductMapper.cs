using VendingMachine.DTOs;
using VendingMachine.Models;

namespace VendingMachine.Mapping
{
	public class BuyerProductMapper
	{
		public static BuyerProductDTO? MapToBuyerProductDTO(BuyerProduct buyerProduct)
		{
			if (buyerProduct is not null)
			{
				BuyerProductDTO buyerProductDTO = new BuyerProductDTO()
				{
					BuyerId = buyerProduct.BuyerId,
					ProductId = buyerProduct.ProductId!,
					Quantity = buyerProduct.Quantity,
					//BuyerDTO = buyerProduct.Buyer is not null ? UserMapper.MapToUserDTO(buyerProduct.Buyer) : null,
					ProductDTO = buyerProduct.Product is not null ? ProductMapper.MapToProductDTO(buyerProduct.Product) : null,
				};
				return buyerProductDTO;
			}
			return null;
		}
		public static BuyerProduct? MapToBuyerProduct(BuyerProductDTO buyerProductDTO)
		{
			if (buyerProductDTO is not null)
			{
				BuyerProduct buyerProduct = new BuyerProduct()
				{
					BuyerId = buyerProductDTO.BuyerId,
					ProductId = buyerProductDTO.ProductId!,
					Quantity = buyerProductDTO.Quantity,
					Buyer = buyerProductDTO.BuyerDTO is not null ? UserMapper.MapToUser(buyerProductDTO.BuyerDTO) : null,
					Product = buyerProductDTO.ProductDTO is not null ? ProductMapper.MapToProduct(buyerProductDTO.ProductDTO) : null,
				};
				return buyerProduct;
			}
			return null;

		}
		public static List<BuyerProductDTO> MapToBuyerProductDTOs(List<BuyerProduct> buyerProducts)
		{
			if (buyerProducts.Any())
			{
				List<BuyerProductDTO> buyerProductDTOs = buyerProducts.Select(buyer => MapToBuyerProductDTO(buyer)).ToList();
				return buyerProductDTOs;
			}
			return new List<BuyerProductDTO>();
		}
		public static List<BuyerProduct> MapToBuyerProducts(List<BuyerProductDTO> buyerProductDTOs)
		{
			if (buyerProductDTOs.Any())
			{
				List<BuyerProduct> buyerProducts = buyerProductDTOs.Select(productDTO => MapToBuyerProduct(productDTO)).ToList();
				return buyerProducts;
			}
			return new List<BuyerProduct>();
		}
	}
}
