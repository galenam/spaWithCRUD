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
        public Department Get(int id)
        {            
            return id> 0 ? _departmentRepository.Get(id) : null;
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
        public bool Put([FromBody]Department department)
        {            
            if (!ModelState.IsValid){return false;}
            return _departmentRepository.Update(department)>0;   
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _departmentRepository.Delete(id)>0;
        }
    }
}
