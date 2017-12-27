using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace CategoriesAndDepartmentsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
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
    }
}