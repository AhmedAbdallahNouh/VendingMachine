using VendingMachine.DTOs;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Mapping
{
    public static class UserMapper
	{
		public static IAppUserDTO? MapToUserDTO(AppUser user, string role = "")
		{
			if(user is not null)
			{
				//if(user.UserRoles.FirstOrDefault(r=> r.))
				if (string.IsNullOrWhiteSpace(role))
					return new AppUserDTO()
					{
						Id = user.Id,
						Role = role,
						UserName = user.UserName,
					};
				if(role == "Seller")
				{
					SellerDTO sellerDTO = new SellerDTO()
					{
						Id = user.Id,
						UserName = user.UserName!,
						SellerProductsDTOs = user.SellerProducts?.Count > 0 ? ProductMapper.MapToProductDTOs(user.SellerProducts.ToList()) : new List<ProductDTO>(),
					};
					return sellerDTO;
				}
				if(role == "Buyer")
				{
					BuyerDTO buyerDTO = new BuyerDTO()
					{
						Id = user.Id,
						UserName = user.UserName!,
						Deposit = user.Deposit,
						BuyerProductDTOs = user.BuyerProducts?.Count > 0 ? user.BuyerProducts.Select(bp => 
											new BuyerProductDTO() { ProductId = bp.ProductId, BuyerId = bp.BuyerId, }).ToList() : new List<BuyerProductDTO>(),
					};

					return buyerDTO;
				}
				return null;
			}
			return null;
			
		}
		public static AppUser? MapToUser(IAppUserDTO userDTO)
		{
			if(userDTO is not null)
			{
				if (userDTO is SellerDTO sellerDTO)
				{
					AppUser user = new AppUser()
					{
						Id = sellerDTO.Id,
						UserName = sellerDTO.UserName,
						SellerProducts = sellerDTO.SellerProductsDTOs?.Count > 0 ? ProductMapper.MapToProducts(sellerDTO.SellerProductsDTOs.ToList()) : new List<Product>(),
					};
					return user;
				}
				if (userDTO is BuyerDTO buyerDTO)
				{
					AppUser user = new AppUser()
					{
						Id = buyerDTO.Id,
						UserName = buyerDTO.UserName,
						BuyerProducts = buyerDTO.BuyerProductDTOs?.Count > 0 ? buyerDTO.BuyerProductDTOs.Select(bp =>
																	new BuyerProduct() { ProductId = bp.ProductId, BuyerId = bp.BuyerId, }).ToList() : new List<BuyerProduct>(),
					};
					return user;
				}
				//if (userDTO is RegistrationUserDTO registrationUserDTO)
				//{
				//	AppUser user = new AppUser()
				//	{
				//		UserName = registrationUserDTO.UserName,
				//	};
				//	return user;
				//}

			}
			return null;
			
		}
		public static AppUser? MapToRegistrationUser(RegistrationUserDTO registrationUserDTO)
		{
			if (registrationUserDTO is not null)
			{	
					AppUser user = new AppUser()
					{
						UserName = registrationUserDTO.UserName,
					};
					return user;		
			}
			return null;

		}

		public static List<IAppUserDTO> MapToUserDTOs(List<AppUser> users , string role = "")
		{
			if(users.Any())
			{
				List<IAppUserDTO> userDTOs = users.Select(user => MapToUserDTO(user,role)).ToList();
				return userDTOs;
			}
			return new List<IAppUserDTO>();
		}
		public static List<AppUser?> MapToUsers(List<IAppUserDTO> userDTOs)
		{
			if (userDTOs.Any())
			{
				List<AppUser?> users = userDTOs.Select(userDTO => MapToUser(userDTO)).ToList();
				return users;
			}
			return new List<AppUser?>();			
		}
	}
}
