using GoFinStrategy.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SoftDelete(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();

            var entities = changeTracker.Entries()
                .Where(t => t.Entity is Entity && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    ((Entity)entry.Entity).Delete();
                    entry.State = EntityState.Modified;
                }
            }
        }

        public static void AddTimestamps(this ChangeTracker changeTracker)
        {
            var entities = changeTracker.Entries()
                .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                var now = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                    ((Entity)entityEntry.Entity).CreatedAt = now;

                ((Entity)entityEntry.Entity).UpdatedAt = now;
            }
        }
    }
}
