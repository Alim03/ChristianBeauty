using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChristianBeauty.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T item);

        Task SaveAsync();
    }
}
