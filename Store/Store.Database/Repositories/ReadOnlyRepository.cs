using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Database.Entities.Base;
using Store.Database.Repositories.Base;

namespace Store.Database.Repositories
{
    public class ReadOnlyRepository<TContext> : IReadOnlyRepository where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly CancellationToken _cancellationToken;

        public ReadOnlyRepository(TContext context,
            CancellationTokenSource cancellationTokenSource)
        {
            _context = context;
            _cancellationToken = cancellationTokenSource.Token;
        }
        public async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null,
            bool isIgnoreQueryFilter = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity
        {
            var entity = await GetQueryable(filter, isDeleted, isIgnoreQueryFilter, orderBy, includeProperties)
                .FirstOrDefaultAsync(_cancellationToken);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task<TEntity[]> GetAllAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null,
            bool isIgnoreQueryFilter = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryableFilter = null)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = GetQueryable(filter, isDeleted, isIgnoreQueryFilter, orderBy,
                includeProperties, queryableFilter: queryableFilter);

            return await query.ToArrayAsync(_cancellationToken);
        }

        public async Task<int> CountAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null)
            where TEntity : class, IEntity
        {
            var query = GetQueryable(filter, isDeleted);


            return await query.CountAsync(_cancellationToken);
        }

        public async Task<bool> ExistsAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null)
            where TEntity : class, IEntity
        {
            bool isIgnoreQueryFilter = false;
            if (isDeleted == null || isDeleted.Value)
                isIgnoreQueryFilter = true;

            return await GetQueryable(filter, isDeleted, isIgnoreQueryFilter: isIgnoreQueryFilter)
                            .AnyAsync(_cancellationToken);
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null,
            bool isIgnoreQueryFilter = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryableFilter = null,
            bool shouldUseOrderBy = true)
            where TEntity : class, IEntity
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                includeProperties = includeProperties.Replace(" ", "");

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            if (filter != null)
                query = query.Where(filter);

            if (isDeleted != null)
                query = query.Where(x => x.IsDeleted == isDeleted);

            if (isIgnoreQueryFilter)
                query = query.IgnoreQueryFilters();

            if (queryableFilter != null)
                query = queryableFilter(query);

            if (shouldUseOrderBy) // default order is below
                query = (orderBy != null) ? orderBy(query) : query.OrderByDescending(x => x.CreatedDate);

            return query;
        }

    }
}
