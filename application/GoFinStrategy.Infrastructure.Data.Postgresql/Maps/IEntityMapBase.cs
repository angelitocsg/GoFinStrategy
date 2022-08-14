using Microsoft.EntityFrameworkCore;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Maps
{
    public interface IEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
    }
}
