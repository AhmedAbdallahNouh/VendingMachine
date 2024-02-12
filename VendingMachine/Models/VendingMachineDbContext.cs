using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VendingMachine.Models
{
	public class VendingMachineDbContext : IdentityDbContext<AppUser>
	{
		public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<BuyerProduct>().HasKey(bp => new { bp.BuyerId , bp.ProductId });
			base.OnModelCreating(builder);
		}
	}
}