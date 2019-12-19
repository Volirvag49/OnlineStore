using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using Serilog;
using Store.Database.Entities.Base;
using Store.Database.Extensions;
using Store.Database.Repositories.Base;

namespace Store.Database.Repositories
{
    public class WriteOnlyRepository<TContext> : IWriteOnlyRepository where TContext : DbContext
    {
        private readonly TContext _context;

        public WriteOnlyRepository(TContext context)
        {
            _context = context;

        }

        public async Task UpdateTracked<TEntity>(TEntity entity)
             where TEntity : class, IEntity
        {
            await UpdateTracked(entity.SingleAsEnumerable());
        }

        public async Task UpdateTracked<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IEntity
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.ModifiedDate = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Log.Error("Exception: Сould not update tracked entities: " + entities.ToString());

                LogInnerException(ex);
                throw;
            }
        }

        public async Task RestoreByIdAsync<TEntity>(Guid id)
            where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            await RestoreEntity(entity);
        }

        public async Task RestoreEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            try
            {
                entity.IsDeleted = false;
                await SaveChangesAsync(entity, $"{nameof(Entity.IsDeleted)}".SingleAsEnumerable());
            }
            catch (Exception ex)
            {
                //Log.Error("Exception: Сould not restore an object: " + typeof(TEntity).Name + "\n Id:" + entity.Id);

                LogInnerException(ex);
                throw;
            }
        }

        public async Task RestoreEntitiesAsync<TEntity>(IEnumerable<TEntity> entities)
           where TEntity : class, IEntity
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                    entity.IsDeleted = false;

                await SaveChangesAsync(entities, $"{nameof(Entity.IsDeleted)}".SingleAsEnumerable());
            }
        }

        public async Task SaveChangesAsync<TEntity>(TEntity entity, IEnumerable<string> propertiesToUpdate = null)
            where TEntity : class, IEntity
        {
            try
            {
                ChangeEntity(entity, propertiesToUpdate);

                await _context.SaveChangesAsync();
                // _context.Entry(entity).State = EntityState.Detached;

            }
            catch (Exception ex)
            {
                //Log.Error("Exception: Сould not create/update object: " + entity);

                LogInnerException(ex);
                throw;
            }
        }

        public async Task SaveChangesAsync<TEntity>(IEnumerable<TEntity> entities,
            IEnumerable<string> propertiesToUpdate = null)
            where TEntity : class, IEntity
        {
            try
            {
                if (entities == null || !entities.Any())
                    return;

                foreach (var entity in entities)
                    ChangeEntity(entity, propertiesToUpdate);

                await _context.SaveChangesAsync();

                //foreach (var entity in entities)
                //    _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
               // Log.Error("Exception: Сould not create/update a collection of objects: " + entities.ToString());

                LogInnerException(ex);
                throw;
            }
        }

        public async Task CreateRangeAsTracking<TEntity>(IEnumerable<TEntity> entities, bool shouldSaveChanges = true)
            where TEntity : class, IEntity
        {
            try
            {
                if (entities == null || !entities.Any())
                    return;

                _context.Set<TEntity>().AddRange(entities);

                if (shouldSaveChanges)
                    await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Log.Error("Exception: Сould not CreateRangeAsTracking: " + typeof(TEntity).Name);

                LogInnerException(ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync<TEntity>(Guid id)
            where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            await DeleteEntityAsync(entity);
        }

        public async Task DeleteEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            if (entity != null)
            {
                entity.IsDeleted = true;
                await SaveChangesAsync(entity, $"{nameof(Entity.IsDeleted)}".SingleAsEnumerable());
            }
        }

        public async Task DeleteEntitiesAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                    entity.IsDeleted = true;

                await SaveChangesAsync(entities, $"{nameof(Entity.IsDeleted)}".SingleAsEnumerable());
            }
        }

        private void ChangeEntity<TEntity>(TEntity entity, IEnumerable<string> propertiesToUpdate = null)
            where TEntity : class, IEntity
        {
            if (entity.Id == Guid.Empty)
            {
                _context.Set<TEntity>().Add(entity);
            }
            else
            {
                entity.ModifiedDate = DateTime.UtcNow;
                _context.Set<TEntity>().Attach(entity);
                if (propertiesToUpdate == null)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }
                else
                {
                    foreach (var propertyToUpdate in propertiesToUpdate)
                    {
                        _context.Entry(entity).Property(propertyToUpdate).IsModified = true;
                    }
                    _context.Entry(entity).Property(x => x.ModifiedDate).IsModified = true;
                }
            }
        }

        // Todo..
        private static void LogInnerException(Exception ex)
        {

        }
    }
}
