using Store.Database.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Database.Repositories.Base
{
    public interface IReadOnlyRepository
    {
        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null,
            bool isIgnoreQueryFilter = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity;

        Task<TEntity[]> GetAllAsync<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           bool? isDeleted = null,
           bool isIgnoreQueryFilter = false,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null,
           Func<IQueryable<TEntity>, IQueryable<TEntity>> queryableFilter = null)
           where TEntity : class, IEntity;

        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null)
            where TEntity : class, IEntity;

        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null)
            where TEntity : class, IEntity;

        IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool? isDeleted = null,
            bool isIgnoreQueryFilter = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> queryableFilter = null,
            bool shouldUseOrderBy = true)
            where TEntity : class, IEntity;
    }
}
