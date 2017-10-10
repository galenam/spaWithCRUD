using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using code.Model;
using Microsoft.Extensions.Logging;

namespace code.Controllers
{
    // todo : сделать внятные описания ошибок. 
    // todo: Обработать exception при работе с БД
//    https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _departmentRepository;
        private readonly ILogger _logger;
        public DepartmentController(IDepartmentRepository dr, ILogger<DepartmentController> logger)
        {
            _departmentRepository = dr;
            _logger = logger;
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
        public async Task<IActionResult> Post([FromBody]Department department)
        {
            if (!ModelState.IsValid){return BadRequest();}
            var reslut = await _departmentRepository.InsertAsync(department);
            if (reslut>0) return new ObjectResult(reslut);
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]Department department)
        {            
            if (!ModelState.IsValid){return BadRequest();}
            if (id != department.Id){return BadRequest();}

            var departmentForUpdate = await _departmentRepository.GetAsync(id);
            departmentForUpdate.Title = department.Title;            
            var result = await _departmentRepository.UpdateAsync(departmentForUpdate);
            if (result) {return Ok();}
            return BadRequest();   
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _departmentRepository.DeleteAsync(id);
        }
    }
}
