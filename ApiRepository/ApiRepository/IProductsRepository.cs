using Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRepository
{
    public interface IProductsRepository
    {
        IEnumerable<Product> Products { get; }
    }
    internal class EFProductRepository : IProductsRepository
    {
        private ApiDbContext _context { get; }

        IEnumerable<Product> IProductsRepository.Products => _context.Products;
        public EFProductRepository(ApiDbContext ctx)
        {
            this._context = ctx;
        }
    }

    public static partial class Export
    {
        public static IServiceCollection UseProductsRepository(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, EFProductRepository>();
            return services;
        }
    }
}