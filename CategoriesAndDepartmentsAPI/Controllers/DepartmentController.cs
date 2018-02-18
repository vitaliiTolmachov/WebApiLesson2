using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ApiRepository;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository;

namespace CategoriesAndDepartmentsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        public IDepartmentRepository Repository { get; set; }
        public DepartmentController(IDepartmentRepository repo)
        {
            Repository = repo;

        }
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return Repository.Departments;
        }
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            return Repository.Departments.SingleOrDefault(p => p.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Department newDept)
        {
            if (ModelState.IsValid)
            {
                EntityEntry<Department> res = await Repository.Add(newDept);
                return Json(res);
            }
            else
            {
                return BadRequest(newDept);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
