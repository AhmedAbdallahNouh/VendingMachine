using System.Linq.Expressions;

namespace VendingMachine.Interfaces
{
    public interface IBaseRepository<T>
    {
        T? Add(T entity);
        IEnumerable<T> AddRnge(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        T? Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T>? FindAll(Expression<Func<T, bool>> criteria);
        IEnumerable<T>? FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T>? FindAll(Expression<Func<T, bool>> criteria, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");
        Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T>? FindAllPagination(Expression<Func<T, bool>> criteria, int page = 1, int size = 10);
        IEnumerable<T>? FindAllPagination(Expression<Func<T, bool>> criteria, int page = 1, int size = 10, string[] includes = null);
        Task<IEnumerable<T>?> FindAllPaginationAsync(Expression<Func<T, bool>> criteria, int page = 1, int size = 10);
        Task<IEnumerable<T>?> FindAllPaginationAsync(Expression<Func<T, bool>> criteria, int page = 1, int size = 10, string[] includes = null);
        Task<T?> FindAsync(Expression<Func<T, bool>> criteria);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAllAsync(string[] includes);
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id, string[] includes = null);
    }
}