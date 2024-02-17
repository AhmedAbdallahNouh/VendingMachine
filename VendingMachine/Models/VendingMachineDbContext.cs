using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace VendingMachine.Models
{
	public class VendingMachineDbContext : IdentityDbContext<AppUser>
	{
		public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options) : base(options)
		{

		}
		public DbSet<AppUser> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<BuyerProduct> BuyersProducts { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<BuyerProduct>().HasKey(bp => new { bp.BuyerId, bp.ProductId });
			base.OnModelCreating(builder);
		}
	}
}