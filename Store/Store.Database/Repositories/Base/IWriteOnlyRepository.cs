using Store.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Database.Repositories.Base
{
    public interface IWriteOnlyRepository
    {
        Task UpdateTracked<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        Task UpdateTracked<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        Task SaveChangesAsync<TEntity>(TEntity entity, IEnumerable<string> propertiesToUpdate = null)
            where TEntity : class, IEntity;
        Task SaveChangesAsync<TEntity>(IEnumerable<TEntity> entities, IEnumerable<string> propertiesToUpdate = null)
            where TEntity : class, IEntity;

        Task CreateRangeAsTracking<TEntity>(IEnumerable<TEntity> entities, bool shouldSaveChanges = true)
            where TEntity : class, IEntity;

        Task DeleteByIdAsync<TEntity>(Guid id)
            where TEntity : class, IEntity;
        Task DeleteEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        Task DeleteEntitiesAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        Task RestoreByIdAsync<TEntity>(Guid id)
            where TEntity : class, IEntity;
        Task RestoreEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
        Task RestoreEntitiesAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;
    }
}
