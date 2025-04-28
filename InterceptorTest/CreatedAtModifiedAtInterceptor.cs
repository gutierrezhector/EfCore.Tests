using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterceptorTest;

public class CreatedAtModifiedAtInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        DateTime utcNow = DateTime.UtcNow;
        var context = eventData.Context;
        var entities = context.ChangeTracker.Entries<ICreatableAndUpdatable>().ToList();

        foreach (EntityEntry<ICreatableAndUpdatable> entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
                entry.Entity.UpdatedAt = utcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}