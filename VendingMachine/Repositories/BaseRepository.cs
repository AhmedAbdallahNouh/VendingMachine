using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VendingMachine.Consts;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly VendingMachineDbContext _context;

		public BaseRepository(VendingMachineDbContext context)
        {
			_context = context;
		}
        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
		public IEnumerable<T> GetAll(string[] includes)
		{
			IQueryable<T> query = _context.Set<T>();
			if(includes is not null)
				foreach(var include in includes)
					query = query.Include(include);

			return _context.Set<T>().ToList();
		}
		public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
		public IEnumerable<T> GetAllAsync(string[] includes)
		{
			IQueryable<T> query = _context.Set<T>();
			if (includes is not null)
				foreach (var include in includes)
					query = query.Include(include);

			return _context.Set<T>().ToList();
		}
		public T? GetById(int id) => _context.Set<T>().Find(id);

		public async Task<T?> GetByIdAsync(int id , string[] includes = null) => await _context.Set<T>().FindAsync(id);

		public T? Find(Expression<Func<T,bool>> match, string[] includes = null) => _context.Set<T>().SingleOrDefault(match);

		public IEnumerable<T>? FindAll(Expression<Func<T, bool>> match) => _context.Set<T>().Where(match).ToList();
		public IEnumerable<T>? FindAll(Expression<Func<T, bool>> match, string[] includes = null) 
			=> _context.Set<T>().Where(match).ToList();

		public IEnumerable<T>? FindAll(Expression<Func<T, bool>> match, string[] includes = null,
			Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending) 
		{
			IQueryable<T> query = _context.Set<T>();
			if (includes is not null)
				foreach (var include in includes)
					query = query.Include(include);
			
			if (orderBy is not null)
			{
				if (orderByDirection == OrderBy.Ascending) 
					query = query.OrderBy(orderBy);
				else 
					query = query.OrderByDescending(orderBy);
			}
			return query.ToList();
		}

		public async Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null) => await _context.Set<T>().Where(match).ToListAsync();

		public  async Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> match) => await _context.Set<T>().Where(match).ToListAsync();

		public IEnumerable<T>? FindAllPagination(Expression<Func<T, bool>> match , int page = 1 , int size = 10) => _context.Set<T>().Where(match).Skip((page - 1) * size).Take(size).ToList();
		
		public IEnumerable<T>? FindAllPagination(Expression<Func<T, bool>> match , int page = 1 , int size = 10 , string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();
			query.Skip((page - 1) * size).Take(size);

			if (includes is not null)
				foreach (var include in includes)
					query = query.Include(include);

			return query.ToList();
		}
		public async Task<IEnumerable<T>?> FindAllPaginationAsync(Expression<Func<T, bool>> match, int page = 1, int size = 10) => await _context.Set<T>().Where(match).Skip((page - 1) * size).Take(size).ToListAsync();

		public async Task<IEnumerable<T>?> FindAllPaginationAsync(Expression<Func<T, bool>> match, int page = 1, int size = 10, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();
			query.Skip((page - 1) * size).Take(size);

			if (includes is not null)
				foreach (var include in includes)
					query = query.Include(include);

			return await query.ToListAsync();
		}
		public async Task<T?> FindAsync(Expression<Func<T, bool>> match) => await _context.Set<T>().SingleOrDefaultAsync(match);

		public T? Add(T entity) 
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
			return entity;
		}
		public IEnumerable<T> AddRnge(IEnumerable<T> entities)
		{
			_context.Set<T>().ad(entities);
			_context.SaveChanges();
			return entity;
		}

		public T? Add(T entity)
		{
			EntityState
			return _context.Set<T>().Entry(id) = EntityState.Modified
		}
	}
}
