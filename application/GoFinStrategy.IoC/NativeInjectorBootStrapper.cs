using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.UseCases.Categories;
using GoFinStrategy.CrossCutting.Shared.Contexts;
using GoFinStrategy.CrossCutting.Shared.Interfaces;
using GoFinStrategy.Domain.Interfaces;
using GoFinStrategy.Domain.Interfaces.Repositories;
using GoFinStrategy.Infrastructure.Data.Postgresql.Context;
using GoFinStrategy.Infrastructure.Data.Postgresql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GoFinStrategy.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Shared Context
            services.AddScoped<INotificationContext, NotificationContext>();

            // Database
            services.AddDbContext<DataContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Use Cases
            services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
            services.AddTransient<IDeleteCategoryByIdUseCase, DeleteCategoryByIdUseCase>();
            services.AddTransient<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();
            services.AddTransient<IGetCategoryByIdUseCase, GetCategoryByIdUseCase>();
            services.AddTransient<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

            // Repositories
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }
    }
}
