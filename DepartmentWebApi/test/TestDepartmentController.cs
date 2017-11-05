using System;
using System.Linq;
using Xunit;
using Moq;
using BaseWebApi.Code.Interfaces;
using System.Threading.Tasks;
using DepartmentWebApi.code.Model;
using System.Collections.Generic;
using DepartmentWebApi.code.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebApi.Tests
{
	// todo : run all tests automatically when build|run main project    
	public class TestDepartmentController
	{
		private List<Department> _departments = new List<Department>();
		[Fact]
		public async void TestGetAll()
		{
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();
			mockDepartmentRepository.Setup(repo => repo.GetAllAsync()).Returns(Task.Run(() => GetFakeDepartment()));
			var dController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			var result = await dController.Get();

			var objectResult = Assert.IsType<ObjectResult>(result);
			var departments = Assert.IsType<List<Department>>(objectResult.Value);
			Assert.Equal(departments.Count, 3);
		}

		[Theory]
		[InlineData(-1, typeof(NotFoundObjectResult))]
		[InlineData(1, typeof(ObjectResult), "Marketing")]
		[InlineData(4, typeof(NotFoundObjectResult))]
		public async void TestGetById(long id, Type t, string title = null)
		{
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();
			var departments = await GetFakeDepartment();

			mockDepartmentRepository.Setup(repo => repo.GetAsync(id)).ReturnsAsync(departments.FirstOrDefault(d => d.Id == id));

			var dController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			var result = await dController.Get(id);
			Assert.IsType(t, result);

			if (t == typeof(ObjectResult))
			{
				var objectResult = Assert.IsType<ObjectResult>(result);
				var department = Assert.IsType<Department>(objectResult.Value);
				Assert.Equal(department.Id, id);
				Assert.Equal(department.Name, title);
			}
		}

		[Theory]
		[InlineData(0, "Test department", typeof(ObjectResult))]
		public async void Post(int id, string title, Type t)
		{
			var department = new Department { Id = id, Name = title };
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();

			mockDepartmentRepository.Setup(repo => repo.InsertAsync(department)).ReturnsAsync(1);
			var departmentController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			var result = await departmentController.Post(department);
			Assert.IsType(t, result);

			if (t == typeof(OkObjectResult))
			{
				var objectResult = Assert.IsType<OkObjectResult>(result);
				var idInserted = Assert.IsType<int>(objectResult.Value);
				Assert.True(idInserted > 0);
			}
		}

		[Theory]
		[InlineData(0, "Test department", 0, true, typeof(OkResult))]
		[InlineData(0, "Test department", 1, true, typeof(BadRequestResult))]
		[InlineData(0, "Test department", 0, false, typeof(BadRequestResult))]
		public async void Put(int id, string titleDepartment, int idDepartment, bool resultUpdate, Type t)
		{
			var department = new Department { Id = idDepartment, Name = titleDepartment };
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();

			mockDepartmentRepository.Setup(repo => repo.UpdateAsync(department)).ReturnsAsync(resultUpdate);
			var departmentController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			var result = await departmentController.Put(id, department);
			Assert.IsType(t, result);

			if (t == typeof(OkObjectResult))
			{
				var objectResult = Assert.IsType<OkObjectResult>(result);
				var repoReturn = Assert.IsType<bool>(objectResult.Value);
				Assert.True(repoReturn);
			}
		}

		[Theory]
		[InlineData(-1, true, typeof(BadRequestResult))]
		[InlineData(1, true, typeof(OkResult))]
		[InlineData(1, false, typeof(BadRequestResult))]
		public async void Delete(int id, bool resultUpdate, Type t)
		{
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();

			mockDepartmentRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(resultUpdate);
			var departmentController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			var result = await departmentController.Delete(id);
			Assert.IsType(t, result);

			if (t == typeof(OkObjectResult))
			{
				var objectResult = Assert.IsType<OkObjectResult>(result);
				var repoReturn = Assert.IsType<bool>(objectResult.Value);
				Assert.True(repoReturn);
			}
		}

		[Fact]
		public async void TestValidationDepartmentModel()
		{
			var mockDepartmentRepository = new Mock<IBaseRepository<Department>>();
			var mockLogger = new Mock<ILogger<DepartmentController>>();
			var department = new Department();

			mockDepartmentRepository.Setup(repo => repo.InsertAsync(department)).ReturnsAsync(FakeInsert(department));
			var departmentController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
			departmentController.ModelState.AddModelError("Name", "Required");
			var result = await departmentController.Post(department);
			Assert.IsType<BadRequestResult>(result);
		}

		private long FakeInsert(Department department)
		{
			if (_departments.FirstOrDefault(d => d.Name == department.Name) != null) { return -1; }
			return 1;
		}

		private async Task<List<Department>> GetFakeDepartment()
		{
			return await Task.Run(() =>
			{
				return new List<Department>{
					new Department{Id=0, Name="It"},
					new Department{Id=1, Name="Marketing"},
					new Department{Id=2, Name="Accountant Department"}
				};
			});
		}
	}
}
