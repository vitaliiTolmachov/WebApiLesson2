using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ProductsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductRepository Repository { get;}
        private IProductService Service { get;}

        public ProductController(IProductRepository repo, IProductService service)
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

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
