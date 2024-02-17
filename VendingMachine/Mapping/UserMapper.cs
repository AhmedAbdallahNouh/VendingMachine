using VendingMachine.DTOs;
using VendingMachine.DTOs.UserDTOs;
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
					UserName = user.UserName!,
					Deposit = user.Deposit,
					SellerProductsDTOs = user.SellerProducts?.Count> 0 ? ProductMapper.MapToProductDTOs(user.SellerProducts.ToList()) : new List<ProductDTO>(),
					//Seller = user.Seller,
					//KeyDTOs = AppUser.Keys.Select(k => MapKeyToDto(k)).ToList()

				};
				return appUserDTO;
			}
			return null;
			
		}
		public static AppUser? MapToUser(BaseUserDTO userDTO)
		{
			if(userDTO is not null)
			{
				if (userDTO is AppUserDTO appUserDTO)
				{
					AppUser user = new AppUser()
					{
						Id = appUserDTO.Id,
						UserName = appUserDTO.UserName,
						Deposit = appUserDTO.Deposit,
						SellerProducts = appUserDTO.SellerProductsDTOs?.Count > 0 ? ProductMapper.MapToProducts(appUserDTO.SellerProductsDTOs.ToList()) : new List<Product>(),
					};
					return user;
				}
				else if (userDTO is RegistrationUserDTO registrationUserDTO)
				{
					AppUser user = new AppUser()
					{
						Id = registrationUserDTO.Id,
						UserName = registrationUserDTO.UserName,
						Deposit = registrationUserDTO.Deposit,
					};
					return user;
				}
				
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
		public static List<AppUser> MapToUsers(List<BaseUserDTO> userDTOs)
		{
			if (userDTOs.Any())
			{
				List<AppUser> users = userDTOs.Select(userDTO => MapToUser(userDTO)).ToList();
				return users;
			}
			return new List<AppUser>();			
		}
	}
}
