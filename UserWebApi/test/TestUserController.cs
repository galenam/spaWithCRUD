using System;
using System.Linq;
using Xunit;
using Moq;
using BaseWebApi.Code.Interfaces;
using System.Threading.Tasks;
using UserWebApi.Code.Model;
using System.Collections.Generic;
using UserWebApi.code.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net;

namespace UserWebApi.Tests
{
	// todo : run all tests automatically when build|run main project    
	public class TestUserController
	{
		private List<User> _users = new List<User>();
		[Fact]
		public async void TestGetAll()
		{
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();
			mockUserRepository.Setup(repo => repo.GetAllAsync()).Returns(Task.Run(() => GetFakeUsers()));
			var dController = new UserController(mockUserRepository.Object, mockLogger.Object);
			var result = await dController.Get();

			var objectResult = Assert.IsType<ObjectResult>(result);
			var users = Assert.IsType<List<User>>(objectResult.Value);
			Assert.Equal(users.Count, 3);
		}

		[Theory]
		[InlineData(-1, typeof(NotFoundObjectResult))]
		[InlineData(1, typeof(ObjectResult), "User2")]
		[InlineData(4, typeof(NotFoundObjectResult))]
		public async void TestGetById(long id, Type t, string name = null)
		{
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();
			var users = await GetFakeUsers();

			mockUserRepository.Setup(repo => repo.GetAsync(id)).ReturnsAsync(users.FirstOrDefault(d => d.Id == id));

			var dController = new UserController(mockUserRepository.Object, mockLogger.Object);
			var result = await dController.Get(id);
			Assert.IsType(t, result);

			if (t == typeof(ObjectResult))
			{
				var objectResult = Assert.IsType<ObjectResult>(result);
				var department = Assert.IsType<User>(objectResult.Value);
				Assert.Equal(department.Id, id);
				Assert.Equal(department.Name, name);
			}
		}

		[Theory]
		[InlineData(0, "Test user", 1, typeof(ObjectResult))]
		public async void Post(int id, string name, int departmentId, Type t)
		{
			var user = new User { Id = id, Name = name, DepartmentId = departmentId };
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();
			Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
			var mockRest = new HttpClient(_fakeHttpMessageHandler.Object);
			mockRest.BaseAddress =new Uri("http://localhost");
			_fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent("{1}")
			});

			mockUserRepository.Setup(repo => repo.InsertAsync(user)).ReturnsAsync(1);
			var userController = new UserController(mockUserRepository.Object, mockLogger.Object, mockRest);
			var result = await userController.Post(user);
			Assert.IsType(t, result);

			if (t == typeof(OkObjectResult))
			{
				var objectResult = Assert.IsType<OkObjectResult>(result);
				var idInserted = Assert.IsType<int>(objectResult.Value);
				Assert.True(idInserted > 0);
			}
		}

		[Theory]
		[InlineData(0, "Test user", 0, 1, true, typeof(OkResult))]
		[InlineData(0, "Test user", 1, 1, true, typeof(BadRequestResult))]
		[InlineData(0, "Test user", 0, 1, false, typeof(BadRequestResult))]
		public async void Put(int id, string titleUser, int idUser, int departmentId, bool resultUpdate, Type t)
		{
			var user = new User { Id = idUser, Name = titleUser, DepartmentId = departmentId };
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();

			Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
			var mockRest = new HttpClient(_fakeHttpMessageHandler.Object);
			mockRest.BaseAddress =new Uri("http://localhost");
			_fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			});


			mockUserRepository.Setup(repo => repo.UpdateAsync(user)).ReturnsAsync(resultUpdate);
			var departmentController = new UserController(mockUserRepository.Object, mockLogger.Object, mockRest);
			var result = await departmentController.Put(id, user);
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
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();

			mockUserRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(resultUpdate);
			var departmentController = new UserController(mockUserRepository.Object, mockLogger.Object);
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
		public async void TestValidationUserModel()
		{
			var mockUserRepository = new Mock<IBaseRepository<User>>();
			var mockLogger = new Mock<ILogger<UserController>>();
			var department = new User();

			mockUserRepository.Setup(repo => repo.InsertAsync(department)).ReturnsAsync(FakeInsert(department));
			var departmentController = new UserController(mockUserRepository.Object, mockLogger.Object);
			departmentController.ModelState.AddModelError("Name", "Required");
			var result = await departmentController.Post(department);
			Assert.IsType<BadRequestResult>(result);
		}

		private long FakeInsert(User department)
		{
			if (_users.FirstOrDefault(d => d.Name == department.Name) != null) { return -1; }
			return 1;
		}

		private async Task<List<User>> GetFakeUsers()
		{
			return await Task.Run(() =>
			{
				return new List<User>{
					new User{Id=0, Name="User1", DepartmentId=0},
					new User{Id=1, Name="User2", DepartmentId=1},
					new User{Id=2, Name="User3", DepartmentId=0}
				};
			});
		}
	}



	public class FakeHttpMessageHandler : HttpMessageHandler
	{
		public virtual HttpResponseMessage Send(HttpRequestMessage request)
		{
			throw new NotImplementedException("Now we can setup this method with our mocking framework");
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			return Task.FromResult(Send(request));
		}
	}
}
