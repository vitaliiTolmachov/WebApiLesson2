using Repository;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }

    internal class EFCategoryRepository: ICategoryRepository
    {
        private ApiDbContext _context { get; }
        IEnumerable<Category> ICategoryRepository.Categories => _context.Categories;
        public EFCategoryRepository(ApiDbContext ctx)
        {
            this._context = ctx;
        }
    }

    public static partial class Export
    {
        public static IServiceCollection UseCategoryRepository(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();
            return services;
        }
    }
}