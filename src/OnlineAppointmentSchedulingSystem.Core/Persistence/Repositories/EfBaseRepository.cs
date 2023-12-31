﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Core.Common;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories
{
	public class EfBaseRepository<TEntity, TContext> : IAsyncRepository<TEntity>
		where TEntity : BaseEntity
		where TContext : DbContext
	{
		protected readonly TContext _context;

		public EfBaseRepository(TContext context)
		{
			_context = context;
		}

		public IQueryable<TEntity> Query()
		{
			return _context.Set<TEntity>();
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
		{
			await _context.AddRangeAsync(entities);
			await _context.SaveChangesAsync();
			return entities;
		}

		public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
		{
			_context.RemoveRange(entities);
			await _context.SaveChangesAsync();
			return entities;
		}

		public async Task<TEntity?> GetAsync(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = Query();
			if (!enableTracking)
				queryable = queryable.AsNoTracking();
			if (include != null)
				queryable = include(queryable);
			if (withDeleted)
				queryable = queryable.IgnoreQueryFilters();
			return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
		{
			_context.UpdateRange(entities);
			await _context.SaveChangesAsync();
			return entities;
		}
	}
}
