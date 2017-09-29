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
        public IActionResult Get()
        {
            return Ok(_departmentRepository.GetAll());            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {      
            var department = id> 0 ? _departmentRepository.Get(id) : null;
            if (department == null){ return NotFound();}  
            return Ok(department);
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
        public IActionResult Put(long id, [FromBody]Department department)
        {            
            if (!ModelState.IsValid){return BadRequest();}
            if (id != department.Id){return BadRequest();}

            var departmentForUpdate = _departmentRepository.Get(id);
            departmentForUpdate.Title = department.Title;            
            var result = _departmentRepository.Update(departmentForUpdate)>0;
            return result ? Ok() : BadRequest();   
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _departmentRepository.Delete(id)>0;
        }
    }
}
