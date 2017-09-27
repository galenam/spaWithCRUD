using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using code.Model;

namespace code.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository dr)
        {
            _departmentRepository = dr;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return _departmentRepository.GetAll();            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Department Get(string title)
        {            
            return !string.IsNullOrEmpty(title) ? _departmentRepository.Get(title) : null;
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody]Department department)
        {
            if (!ModelState.IsValid){return false;}
            return _departmentRepository.Insert(department) > 0;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Department department)
        {
            /*
            if (!ModelState.IsValid){return false;}
            return _departmentRepository.Update(id, department)>0;
            */
            return false;
        }

        // DELETE api/values/it
        [HttpDelete("{title}")]
        public bool Delete(string title)
        {
            return _departmentRepository.Delete(title)>0;
        }
    }
}
