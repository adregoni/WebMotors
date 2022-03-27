using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMotors.Data.Contexts;
using WebMotors.Domain.Contracts.Repository;

namespace WebMotors.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly Lazy<DbSet<TEntity>> _dbSet;

        public Repository(WebMotorsContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbSet = new Lazy<DbSet<TEntity>>(() => dbContext.Set<TEntity>());
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var added = await _dbSet.Value.AddAsync(entity, cancellationToken);
            return added.Entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = FilterAndOrder<object>(predicate, null, true, null);

            return await query.FirstOrDefaultAsync();
        }
       
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.Value.ToListAsync();
        

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = FilterAndOrder<object>(predicate, null);

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(TEntity entities) => await Task.FromResult(_dbSet.Value.Remove(entities));
       
        public async Task UpdateAsync(TEntity entity) => await Task.FromResult(_dbSet.Value.Update(entity));
        
        private IQueryable<TEntity> FilterAndOrder<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Value.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderByKeySelector != null)
            {
                if (orderAscending)
                {
                    query = query.OrderBy(orderByKeySelector);
                }
                else
                {
                    query = query.OrderByDescending(orderByKeySelector);
                }
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
