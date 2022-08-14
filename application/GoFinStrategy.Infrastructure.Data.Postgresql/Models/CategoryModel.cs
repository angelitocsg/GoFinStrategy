using GoFinStrategy.Domain.Entites;
using GoFinStrategy.Domain.ValueObjects;
using GoFinStrategy.Infrastructure.Data.Postgresql.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Models
{
    public class CategoryModel : EntityMapBase<Category>
    {
        readonly string _tableName = "Categories";

        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.ToTable(_tableName);
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Name).HasConversion(v => v.ToString(), v => new NameVO(v));

            builder.HasIndex(entity => entity.Name).IsUnique();

            builder.Ignore(entity => entity.Notifications);
            builder.Ignore(entity => entity.IsValid);
        }
    }
}
