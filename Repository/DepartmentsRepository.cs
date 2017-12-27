using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;

namespace Domain
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> Departments { get; set; }
        bool Add(Department dept);
    }
    public class FakeDepartmentsRepository : IDepartmentRepository
    {
        public IEnumerable<Department> Departments { get; set; }

        public FakeDepartmentsRepository()
        {
            Departments = new[]
            {
                new Department {Id = 1, Name = "Gym Dept"},
                new Department {Id = 2, Name = "Swimming Dept"}
            };
        }
        public bool Add(Department dept)
        {
            Departments.Append(dept);
            return true;
        }

    }
}
