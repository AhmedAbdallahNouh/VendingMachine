﻿using VendingMachine.DTOs.UserDTOs;

using System.ComponentModel.DataAnnotations.Schema;
using VendingMachine.Models;

namespace VendingMachine.DTOs
{
    public class BuyerProductDTO
	{
		public int ProductId { get; set; }
		public string BuyerId { get; set; }
		public int Quantity { get; set; }
		public BuyerDTO? BuyerDTO { get; set; }
		public ProductDTO? ProductDTO { get; set; }
	}
}
