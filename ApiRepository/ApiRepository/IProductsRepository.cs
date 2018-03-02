using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRepository
{
    public interface IProductsRepository
    {
        IEnumerable<Product> Products { get; }
        int AddProduct(Product product);
        Task<Product> RemoveProductById(int id);
        Task<Product> UpdateProductAsync(Product product);
    }
    internal class EFProductRepository : IProductsRepository
    {
        async Task<Product> IProductsRepository.RemoveProductById(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product newProduct)
        {
            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        private ApiDbContext _context { get; }

        IEnumerable<Product> IProductsRepository.Products =>
            _context.Products.Include(product => product.Category);
        public EFProductRepository(ApiDbContext ctx)
        {
            this._context = ctx;
        }

        int IProductsRepository.AddProduct(Product product)
        {
            var res = _context.Products.Add(product);
            return _context.SaveChanges();
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