using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;

namespace Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; set; }
        bool AddProduct(Product product);
    }
    public class FakeProductRepository : IProductRepository
    {
        public FakeProductRepository()
        {
            var gymDept = new Department { Id = 1, Name = "Gym Dept" };
            var shakers = new Category { Id = 1, Name = "Shakers", Department = gymDept };
            var dumbbells = new Category { Id = 2, Name = "Dumbbells", Department = gymDept };

            var swimmingDept = new Department { Id = 2, Name = "Swimming Dept" };
            var trunks = new Category { Id = 1, Name = "Swimming Trunks", Department = swimmingDept };
            var swimmingHats = new Category { Id = 2, Name = "Swimming Hats", Department = swimmingDept };



            Products = new[]
            {
                new Product { Id = 1, Name = "Shaker Red",Category = shakers},
                new Product { Id = 2, Name = "Shaker Blue", Category = shakers},
                new Product { Id = 3, Name = "Dumbbell 18kg", Category = dumbbells},
                new Product { Id = 4, Name = "Dumbbell 20kg", Category = dumbbells},
                new Product { Id = 5, Name = "SwimmingTrunks S", Category = trunks},
                new Product { Id = 6, Name = "SwimmingTrunks M", Category = trunks},
                new Product { Id = 7, Name = "SwimmingHat Red", Category = swimmingHats},
                new Product { Id = 8, Name = "SwimmingHat Blue", Category = swimmingHats}
            };
        }

        public IEnumerable<Product> Products { get; set; }
        public bool AddProduct(Product product)
        {
            Products.Append(product);
            return true;
        }
    }
}
