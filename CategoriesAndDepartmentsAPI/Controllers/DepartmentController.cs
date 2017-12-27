using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace CategoriesAndDepartmentsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
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

        public IEnumerable<Department> GetById(int id)
        {
            return Repository.Departments.Where(p => p.Id.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public void Create([FromBody]Department newDept)
        {
            bool deptAlreadyExists = Repository.Departments.Any(dept =>
            {
                bool idExists = dept.Id.Equals(newDept.Id);
                bool nameExists = string.Equals(dept.Name, newDept.Name, StringComparison.InvariantCultureIgnoreCase);
                return idExists && nameExists;
            });
            if (deptAlreadyExists)
                return;
            Repository.Add(newDept);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
