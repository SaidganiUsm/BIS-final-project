﻿using Microsoft.EntityFrameworkCore;

namespace OnlineAppointmentSchedulingSystem.Core.Persistence.Paging
{
	public static class IQueryablePaginateExtensions
	{
		public static async Task<IPaginate<T>> ToPaginateAsync<T>(
		this IQueryable<T> source,
		int index,
		int size,
		int from = 0,
		CancellationToken cancellationToken = default
		)
		{
			if (from > index)
				throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

			int count = await source.CountAsync(cancellationToken);

			List<T> items = await source.Skip((index - from) * size).Take(size).ToListAsync(cancellationToken);

			Paginate<T> list =
				new()
				{
					Index = index,
					Size = size,
					From = from,
					Count = count,
					Items = items,
					Pages = (int)Math.Ceiling(count / (double)size)
				};
			return list;
		}
}
