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
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return Repository.Products.SingleOrDefault(product => product.Id.Equals(id));
        }
        [HttpGet("{id}")]
        public Product GetProductsByCategoryId(int id)
        {
            return Repository.Products.SingleOrDefault(product => product.Category.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                Category category = await Service.Get<Category>($"http://localhost:5002/api/category/get", product.CategoryId.ToString());
                if (category != null)
                {
                    Repository.AddProduct(product);
                    return new JsonResult(product);
                }
                return BadRequest(product);
            }
            return BadRequest(product);
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<Product> Delete(int id)
        {
            return await Repository.RemoveProductById(id);
        }
    }
}
