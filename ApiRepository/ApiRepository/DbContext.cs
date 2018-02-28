using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace ApiRepository
{
    public class ApiDbContext : DbContext, IDesignTimeDbContextFactory<ApiDbContext>
    {
        private IConfiguration Configuration { get; }
        
        public ApiDbContext(IConfiguration config, DbContextOptions<ApiDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
            Configuration = config;
        }
        public ApiDbContext(DbContextOptions<ApiDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public ApiDbContext()
        {
            
        }
        //Creates DBContext as Factory on Startup.cs when buildeing host
        public ApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            optionsBuilder.UseSqlServer("Server=ESPC007;Database=ProductApi;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ApiDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>();
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Department)
                .WithMany(department => department.Categories)
                .HasForeignKey(category => category.DepartmentId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Server=ESPC007;Database=ProductApi;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    public static class ApiDbInitializer
    {
        //Store data to Db Tables for exaple if empty
        public static async Task Initialize(ApiDbContext ctx)
        {
            await ctx.Database.EnsureCreatedAsync();

            var gymDept = new Department { Name = "Gym Dept" };
            var swimmingDept = new Department { Name = "Swimming Dept" };

            var shakers = new Category { Name = "Shakers", Department = gymDept };
            var dumbbells = new Category { Name = "Dumbbells", Department = gymDept };
            var swimmingTrunks = new Category { Name = "Swimming Trunks", Department = swimmingDept };
            var swimmingHats = new Category { Name = "Swimming Hats", Department = swimmingDept };

            var prod1 = new Product { Name = "Shaker Red", Category = shakers };
            var prod2 = new Product { Name = "Shaker Blue", Category = shakers };
            var prod3 = new Product { Name = "Dumbbell 18kg", Category = dumbbells };
            var prod4 = new Product { Name = "Dumbbell 20kg", Category = dumbbells };
            var prod5 = new Product { Name = "SwimmingTrunks S", Category = swimmingTrunks };
            var prod6 = new Product { Name = "SwimmingTrunks M", Category = swimmingTrunks };
            var prod7 = new Product { Name = "SwimmingHat Red", Category = swimmingHats };
            var prod8 = new Product { Name = "SwimmingHat Blue", Category = swimmingHats };

            if (await ctx.Departments.CountAsync() == 0)
            {
                await ctx.Departments.AddRangeAsync(gymDept, swimmingDept);
            }

            if (await ctx.Categories.CountAsync() == 0)
            {
                await ctx.Categories.AddRangeAsync(shakers, dumbbells, swimmingTrunks, swimmingHats);
            }

            if (await ctx.Products.CountAsync() == 0)
            {
                await ctx.Products.AddRangeAsync(prod1, prod2, prod3, prod4, prod5, prod6, prod7, prod8);
            }

            await ctx.SaveChangesAsync();
        }

        //Store data to Db Tables for exaple if empty
        public static async Task<int> StoreDataToDb(IApplicationBuilder appBuilder)
        {
            var gymDept = new Department { Name = "Gym Dept" };
            var swimmingDept = new Department { Name = "Swimming Dept" };

            var shakers = new Category { Name = "Shakers", Department = gymDept };
            var dumbbells = new Category { Name = "Dumbbells", Department = gymDept };
            var swimmingTrunks = new Category { Name = "Swimming Trunks", Department = swimmingDept };
            var swimmingHats = new Category { Name = "Swimming Hats", Department = swimmingDept };

            var prod1 = new Product { Name = "Shaker Red", Category = shakers };
            var prod2 = new Product { Name = "Shaker Blue", Category = shakers };
            var prod3 = new Product { Name = "Dumbbell 18kg", Category = dumbbells };
            var prod4 = new Product { Name = "Dumbbell 20kg", Category = dumbbells };
            var prod5 = new Product { Name = "SwimmingTrunks S", Category = swimmingTrunks };
            var prod6 = new Product { Name = "SwimmingTrunks M", Category = swimmingTrunks };
            var prod7 = new Product { Name = "SwimmingHat Red", Category = swimmingHats };
            var prod8 = new Product { Name = "SwimmingHat Blue", Category = swimmingHats };


            var ctx = appBuilder.ApplicationServices.GetRequiredService<ApiDbContext>();

            if (await ctx.Departments.CountAsync() == 0)
            {
                await ctx.Departments.AddRangeAsync(gymDept, swimmingDept);
            }

            if (await ctx.Categories.CountAsync() == 0)
            {
                await ctx.Categories.AddRangeAsync(shakers, dumbbells, swimmingTrunks, swimmingHats);
            }

            if (await ctx.Products.CountAsync() == 0)
            {
                await ctx.Products.AddRangeAsync(prod1, prod2, prod3, prod4, prod5, prod6, prod7, prod8);
            }

            int records = await ctx.SaveChangesAsync();
            return records;
        }
    }
}
