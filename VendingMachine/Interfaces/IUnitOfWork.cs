using VendingMachine.Models;
using Product = VendingMachine.Models.Product;

namespace VendingMachine.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IBaseRepository<Product> Product {  get; }
		IBaseRepository<AppUser> User {  get; }

		int Complete();
	}
}
