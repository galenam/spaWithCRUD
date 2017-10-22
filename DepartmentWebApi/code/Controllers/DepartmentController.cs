using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DepartmentWebApi.code.Interfaces;
using DepartmentWebApi.code.Model;
using Microsoft.Extensions.Logging;
using DepartmentWebApi.code.Constants;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebApi.code.Controllers
{
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
            if (id <=0) {
                _logger.LogWarning(LoggingEvents.DepartmentWebApiGetIncorrectId, $"HttpGet with bad {id}");
                return NotFound(id);
            }
            var departmentTask = await  _departmentRepository.GetAsync(id);
    
            if (departmentTask == null){ 
                _logger.LogError(LoggingEvents.DepartmentWebApiGetNoSuchIdInDB, $"HttpGet no such department id= {id}");
                return NotFound(id);
            }  
            return new ObjectResult(departmentTask);            
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Department department)
        {
            if (!ModelState.IsValid){
                return BadRequest();
            }
            var reslut = await _departmentRepository.InsertAsync(department);
            if (reslut>0) {return new ObjectResult(reslut);}            
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]Department department)
        {            
            if (!ModelState.IsValid){return BadRequest();}
            if (id != department.Id){
                _logger.LogError(LoggingEvents.DepartmentWebApiGetIncorrectId, $"HttpPut error id= {id}, department.Id={department.Id} ");                
                return BadRequest();
            }                     
            var result = await _departmentRepository.UpdateAsync(department);
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
