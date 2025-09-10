using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Data.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		if (eventData.Context is null)
		{
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		var entries = eventData.Context.ChangeTracker.Entries<ISoftDeleteable>().Where(e => e.State == EntityState.Deleted);

		foreach (var entry in entries)
		{


			entry.State = EntityState.Modified;
			entry.Entity.Delete();

		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}