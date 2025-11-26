using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<T?> GetByIdAsync(int id, bool igonereFilters = false);
        Task<IEnumerable<T>> GetAllAsync(bool isTrack = true, bool igonereFilters = false);
        Task<IEnumerable<T>> FindConditionAsync(Expression<Func<T, bool>> predicate, bool isTrack = true, bool igonereFilters = false);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTrack = true, bool igonereFilters = false);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); 
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null); 
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
    }
}
