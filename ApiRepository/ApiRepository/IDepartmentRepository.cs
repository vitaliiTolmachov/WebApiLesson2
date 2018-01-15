using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace ApiRepository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> Departments { get;}

        Task<EntityEntry<Department>> Add(Department newDept);
    }
    internal class EFDepartmentRepository : IDepartmentRepository
    {
        private ApiDbContext _context { get; }
        public IEnumerable<Department> Departments => _context.Departments;
        async Task<EntityEntry<Department>> IDepartmentRepository.Add(Department newDept)
        {
            EntityEntry<Department> createdDept = await _context.Departments.AddAsync(newDept);
            await _context.SaveChangesAsync();
            return createdDept;
        }

        public EFDepartmentRepository(ApiDbContext ctx)
        {
            this._context = ctx;
        }
    }
    public static partial class Export
    {
        public static IServiceCollection UseDepartmentRepository(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, EFDepartmentRepository>();
            return services;
        }
    }
}