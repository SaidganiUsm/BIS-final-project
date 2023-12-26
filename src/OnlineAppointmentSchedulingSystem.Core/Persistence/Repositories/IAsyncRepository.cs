using Auctionify.Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using OnlineAppointmentSchedulingSystem.Core.Common;
using System.Linq.Expressions;

namespace OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories
{
	public interface IAsyncRepository<TEntity> : IQuery<TEntity>
		where TEntity : BaseEntity
	{
		Task<TEntity?> GetAsync(
		Expression<Func<TEntity, bool>> predicate,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
		bool withDeleted = false,
		bool enableTracking = true,
		CancellationToken cancellationToken = default);

		/// <summary>
		/// The signature of this method is not final yet, it will be updated at pagination stage
		/// The same is true with withDeleted param. It will be updated based on our apps deletion behaviour
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="orderBy"></param>
		/// <param name="include"></param>
		/// <param name="index"></param>
		/// <param name="size"></param>
		/// <param name="withDeleted"></param>
		/// <param name="enableTracking"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>

		Task<TEntity> AddAsync(TEntity entity);

		Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

		Task<TEntity> UpdateAsync(TEntity entity);

		Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

		Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);

		Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);
	}
}
