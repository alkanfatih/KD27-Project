using Domain.Commons;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Contracts
{
    public class EfRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EfRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if(predicate == null)
                return _dbSet.CountAsync();
            else
                return _dbSet.Where(predicate).CountAsync();
        }

        public void Delete(T entity)
        {
           _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindConditionAsync(Expression<Func<T, bool>> predicate, bool isTrack = true, bool igonereFilters = false)
        {
            IQueryable<T> query = CheckControl(isTrack, igonereFilters);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTrack = true, bool igonereFilters = false)
        {
            IQueryable<T> query = CheckControl(isTrack, igonereFilters);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool isTrack = true, bool igonereFilters = false)
        {
            IQueryable<T> query = CheckControl(isTrack, igonereFilters);
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, bool igonereFilters = false)
        {
            if(igonereFilters)
                return await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);

            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void SoftDelete(T entity)
        {
            entity.SoftDelete();
            Update(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity); 
        }

        private IQueryable<T> CheckControl(bool isTrack, bool igonereFilters)
        {
            IQueryable<T> query = _dbSet;

            if (igonereFilters)
                query = query.IgnoreQueryFilters();

            if (!isTrack)
                query = query.AsNoTracking();
            return query;
        }
    }
}
