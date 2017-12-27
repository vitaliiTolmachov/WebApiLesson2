using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace Domain
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; set; }

    }
    public class FakeCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories { get; set; }

        public FakeCategoryRepository()
        {
            var gymDept = new Department { Id = 1, Name = "Gym Dept" };
            var swimmingDept = new Department { Id = 2, Name = "Swimming Dept" };

            Categories = new[]
            {
                new Category { Id = 1, Name = "Shakers", Department = gymDept },
                new Category { Id = 2, Name = "Dumbbells", Department = gymDept },
                new Category { Id = 1, Name = "Swimming Trunks", Department = swimmingDept },
                new Category { Id = 2, Name = "Swimming Hats", Department = swimmingDept }
            };
        }
    }
}
