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
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _departmentRepository.GetAllAsync());            
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {      
            if (id <=0) {return NotFound(id);}
            var departmentTask = await  _departmentRepository.GetAsync(id);
    
            if (departmentTask == null){ return NotFound(id);}  
            return new ObjectResult(departmentTask);            
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
            if (result) {return Ok();}
            return BadRequest();   
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _departmentRepository.Delete(id)>0;
        }
    }
}
