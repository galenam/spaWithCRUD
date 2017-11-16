using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseWebApi.Code.Interfaces;
using UserWebApi.Code.Model;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.AbstractClasses;
using System.ComponentModel.DataAnnotations;
using BaseWebApi.Code.Constants;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace UserWebApi.code.Controllers
{
	// todo : добавить тесты UserWebApi, вынести их в общий класс BaseTest
	[Route("api/[controller]")]
	public class UserController : BaseController<User>
	{
		private readonly HttpClient Client;
		public UserController(IBaseRepository<User> dr, ILogger<UserController> logger, HttpClient _client) :
			base(dr, logger)
		{
			Client = _client;
		}
		// POST api/values
		[HttpPost]
		public override async Task<IActionResult> Post([FromBody]User model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			if (Client == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			var departmentAnswer = await Client.GetAsync($"api/department/{model.DepartmentId}");
			if (!departmentAnswer.IsSuccessStatusCode)
			{
				return BadRequest("No such department ID");
			}

			var reslut = await _baseRepository.InsertAsync(model);
			if (reslut > 0) { return new ObjectResult(reslut); }
			return BadRequest();
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public override async Task<IActionResult> Put(long id, [FromBody]User model)
		{
			if (!ModelState.IsValid) { return BadRequest(); }
			if (id != model.Id)
			{
				_logger.LogError(LoggingEvents.GetIncorrectId, $"HttpPut error id= {id}, model.Id={model.Id} ");
				return BadRequest();
			}
			if (Client == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			var departmentAnswer = await Client.GetAsync($"api/department/{model.DepartmentId}");
			if (!departmentAnswer.IsSuccessStatusCode)
			{
				return BadRequest("No such department ID");
			}

			var result = await _baseRepository.UpdateAsync(model);
			if (result) { return new OkResult(); }
			return BadRequest();
		}
	}
}
