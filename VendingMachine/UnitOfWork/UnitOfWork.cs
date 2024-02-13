using VendingMachine.Interfaces;
using VendingMachine.Models;
using VendingMachine.Repositories;

namespace VendingMachine.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly VendingMachineDbContext _context;

		public UnitOfWork(VendingMachineDbContext context)
        {
			_context = context;
			Product = new BaseRepository<Product>(_context);
			User = new BaseRepository<AppUser>(_context);
		}
        public IBaseRepository<Product> Product  {get; private set;}
		public IBaseRepository<AppUser> User { get; private set; }

		public int Complete()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
