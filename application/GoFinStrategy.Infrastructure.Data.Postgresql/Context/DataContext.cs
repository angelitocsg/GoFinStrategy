using GoFinStrategy.Infrastructure.Data.Postgresql.Extensions;
using GoFinStrategy.Infrastructure.Data.Postgresql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GoFinStrategy.Infrastructure.Data.Postgresql.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            builder.AddJsonFile("appsettings.Development.json");
            var configuration = builder.Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgresql"), b => b.MigrationsAssembly("GoFinStrategy.API"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryModel());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.SoftDelete();
            ChangeTracker.AddTimestamps();
            return base.SaveChanges();
        }
    }
}
