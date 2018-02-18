using System.Collections.Generic;
using System.Linq;
using ApiRepository;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace CategoriesAndDepartmentsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository Repository { get;}

        public CategoryController(ICategoryRepository repo)
        {
            Repository = repo;
        }
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return Repository.Categories;
        }
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return Repository.Categories.SingleOrDefault(p => p.Id.Equals(id));
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}