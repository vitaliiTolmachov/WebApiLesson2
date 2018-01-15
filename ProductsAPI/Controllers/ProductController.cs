using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRepository;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ProductsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductsRepository Repository { get;}
        private IProductService Service { get;}

        public ProductController(IProductsRepository repo, IProductService service)
        {
            Repository = repo;
            Service = service;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Repository.Products;
        }

        // GET api/values/5
        [HttpGet]
        public IEnumerable<Product> GetById(int id)
        {
            return Repository.Products.Where(p => p.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public async Task Create([FromBody]Product product)
        {
            List<Department> depts = await Service.GetDepts();
            List<Category> categories = await Service.GetCategories();
            Service.AddProduct(product, categories);
            Repository.Products.Append(product);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
