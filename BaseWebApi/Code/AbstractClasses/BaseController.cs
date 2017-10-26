using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using BaseWebApi.Code.Interfaces;

namespace BaseWebApi.code.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller
	where T : IModel
    {
        private IBaseRepository<T> _baseRepository;
        private readonly ILogger<BaseController<T>> _logger;
        public BaseController(IBaseRepository<T> br, ILogger<BaseController<T>> logger)
        {
            _baseRepository = br;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]        
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _baseRepository.GetAllAsync());            
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {      
            if (id <=0) {
                _logger.LogWarning(LoggingEvents.DepartmentWebApiGetIncorrectId, $"HttpGet with bad {id}");
                return NotFound(id);
            }
            var baseTask = await  _baseRepository.GetAsync(id);
    
            if (baseTask == null){ 
                _logger.LogError(LoggingEvents.DepartmentWebApiGetNoSuchIdInDB, $"HttpGet no such model id= {id}");
                return NotFound(id);
            }  
            return new ObjectResult(baseTask);            
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]T model)
        {
            if (!ModelState.IsValid){
                return BadRequest();
            }
            var reslut = await _baseRepository.InsertAsync(model);
            if (reslut>0) {return new ObjectResult(reslut);}            
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]T model)
        {            
            if (!ModelState.IsValid){return BadRequest();}
            if (id != model.Id){
                _logger.LogError(LoggingEvents.DepartmentWebApiGetIncorrectId, $"HttpPut error id= {id}, model.Id={model.Id} ");                
                return BadRequest();
            }                     
            var result = await _baseRepository.UpdateAsync(model);
            if (result) {return new OkResult();}
            return BadRequest();   
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id<=0) {return BadRequest();}
            var result = await _baseRepository.DeleteAsync(id);
            if (result) return new OkResult();
            return BadRequest();
        }

    
    }
}
