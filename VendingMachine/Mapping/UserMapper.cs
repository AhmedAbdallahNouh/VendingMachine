using VendingMachine.DTOs;
using VendingMachine.Models;

namespace VendingMachine.Mapping
{
	public static class UserMapper
	{
		public static AppUserDTO? MapToUserDTO(AppUser user)
		{
			if(user is not null)
			{
				AppUserDTO appUserDTO = new AppUserDTO()
				{
					Id = user.Id,
					Username = user.UserName!,
					Deposit = user.Deposit,
					SellerProductsDTOs = user.SellerProducts?.Count> 0 ? ProductMapper.MapToProductDTOs(user.SellerProducts.ToList()) : new List<ProductDTO>(),
					//Seller = user.Seller,
					//KeyDTOs = AppUser.Keys.Select(k => MapKeyToDto(k)).ToList()

				};
				return appUserDTO;
			}
			return null;
			
		}
		public static AppUser? MapToUser(AppUserDTO userDTO)
		{
			if(userDTO is not null)
			{
				AppUser user = new AppUser()
				{
					Id = userDTO.Id,
					UserName = userDTO.Username,
					Deposit = userDTO.Deposit,
					SellerProducts = userDTO.SellerProductsDTOs?.Count> 0 ? ProductMapper.MapToProducts(userDTO.SellerProductsDTOs.ToList()) : new List<Product>(),
				};
				return user;
			}
			return null;
			
		}
		public static List<AppUserDTO> MapToUserDTOs(List<AppUser> users)
		{
			if(users.Any())
			{
				List<AppUserDTO> userDTOs = users.Select(user => MapToUserDTO(user)).ToList();
				return userDTOs;
			}
			return new List<AppUserDTO>();
		}
		public static List<AppUser> MapToUsers(List<AppUserDTO> userDTOs)
		{
			if (userDTOs.Any())
			{
				List<AppUser> products = userDTOs.Select(productDTO => MapToUser(productDTO)).ToList();
				return products;
			}
			return new List<AppUser>();			
		}
	}
}
