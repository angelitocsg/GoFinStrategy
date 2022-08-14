using GoFinStrategy.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Maps
{
    public abstract class EntityMapBase<TEntity> : IEntityMap<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
