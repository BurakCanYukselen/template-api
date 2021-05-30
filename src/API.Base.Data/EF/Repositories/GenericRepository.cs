using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Base.Core.Extensions;
using API.Base.Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Base.Data.EF.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<PagedEntityList<TEntity>> GetPagedList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunction, int pageSize, int pageIndex, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<bool> Add(TEntity entity);
        Task<bool> Add(List<TEntity> entities);
        Task<bool> Remove(TEntity entity);
        Task<bool> Remove(List<TEntity> entities);
        Task<bool> Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        Task<bool> Update(List<TEntity> entities, params Expression<Func<TEntity, object>>[] properties);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<int> Count(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties);
        (Expression<Func<TEntity, bool>> filterExpression, List<Expression<Func<TEntity, object>>> navigationProperties) BuildFilter<TFilter>(TFilter filter);
    }

    public abstract class GenericRepository<TDBContext, TEntity> : IGenericRepository<TEntity>
        where TDBContext : DbContext
        where TEntity : class
    {
        protected readonly TDBContext _context;

        public GenericRepository(TDBContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var entities = _context.Set<TEntity>().AsNoTracking();
            if (!navigationProperties.IsNullOrEmpty())
                entities = navigationProperties.Aggregate(entities, (current, navigationProperty) => current.Include(navigationProperty));
            return await entities.FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var entities = _context.Set<TEntity>().AsNoTracking();

            if (!navigationProperties.IsNullOrEmpty())
                entities = navigationProperties.Aggregate(entities, (current, navigationProperty) => current.Include(navigationProperty));

            if (filter == null)
                return await entities.ToListAsync();
            else
                return await entities.Where(filter).ToListAsync();
        }

        public async Task<PagedEntityList<TEntity>> GetPagedList(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunction, int pageSize, int pageIndex, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var entities = _context.Set<TEntity>().AsNoTracking();

            if (!navigationProperties.IsNullOrEmpty())
                entities = navigationProperties.Aggregate(entities, (current, navigationProperty) => current.Include(navigationProperty));

            var queriedList = queryFunction(entities);
            var entityCount = await queriedList.CountAsync();
            var pageCount = (int) decimal.Ceiling((decimal) (entityCount) / pageSize);
            var entityList = await queriedList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedEntityList<TEntity>()
            {
                List = entityList,
                CurrentPage = pageIndex,
                PageCount = pageCount,
                PageSize = pageSize,
            };
        }

        public async Task<bool> Add(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return await SaveChangesAsync();
        }

        public async Task<bool> Add(List<TEntity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Added;
            return await SaveChangesAsync();
        }

        public async Task<bool> Remove(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<bool> Remove(List<TEntity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<bool> Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            _context.Set<TEntity>().Attach(entity);
            var entityToUpdate = _context.Entry(entity);
            if (!properties.IsNullOrEmpty())
                foreach (var property in properties)
                    entityToUpdate.Property(property).IsModified = true;
            else
                _context.Entry(entity).State = EntityState.Modified;

            return await SaveChangesAsync();
        }

        public async Task<bool> Update(List<TEntity> entities, params Expression<Func<TEntity, object>>[] properties)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Attach(entity);
                var entityToUpdate = _context.Entry(entity);
                if (!properties.IsNullOrEmpty())
                    foreach (var property in properties)
                        entityToUpdate.Property(property).IsModified = true;
                else
                    _context.Entry(entity).State = EntityState.Modified;
            }

            return await SaveChangesAsync();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var entities = _context.Set<TEntity>().AsNoTracking();

            if (!navigationProperties.IsNullOrEmpty())
                entities = navigationProperties.Aggregate(entities, (current, navigationProperty) => current.Include(navigationProperty));

            if (filter == null)
                return await entities.AnyAsync();
            else
                return await entities.Where(filter).AnyAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var entities = _context.Set<TEntity>().AsNoTracking();

            if (!navigationProperties.IsNullOrEmpty())
                entities = navigationProperties.Aggregate(entities, (current, navigationProperty) => current.Include(navigationProperty));

            if (filter == null)
                return await entities.CountAsync();
            else
                return await entities.Where(filter).CountAsync();
        }

        public abstract (Expression<Func<TEntity, bool>> filterExpression, List<Expression<Func<TEntity, object>>> navigationProperties) BuildFilter<TFilter>(TFilter filter);

        private async Task<bool> SaveChangesAsync()
        {
            var savedEntityCount = await _context.SaveChangesAsync();
            return savedEntityCount > 0;
        }
    }
}