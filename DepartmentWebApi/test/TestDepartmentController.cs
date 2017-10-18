using System;
using Xunit;
using Moq;
using DepartmentWebApi.code.Interfaces;
using System.Threading.Tasks;
using DepartmentWebApi.code.Model;
using System.Collections.Generic;
using DepartmentWebApi.code.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentWebApi.Tests
{
    public class TestDepartmentController
    {
        [Fact]
        public async void TestGetAll()
        {
            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            var mockLogger = new Mock<ILogger<DepartmentController>>();
            mockDepartmentRepository.Setup(repo=> repo.GetAllAsync()).Returns(Task.Run(()=>GetFakeDepartment()));
            var dController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
            var result = await dController.Get();

            var objectResult = Assert.IsType<ObjectResult>(result);
            var departments = Assert.IsType<List<Department>>(objectResult.Value);
            Assert.Equal(departments.Count, 3);
        }

        [Theory]
        [InlineData(-1, typeof(NotFoundResult))]
        [InlineData(1, typeof(ObjectResult))]
        [InlineData(4, typeof(NotFoundResult))]
        public async void TestGetById(long id, Type t)
        {
            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            var mockLogger = new Mock<ILogger<DepartmentController>>();
            mockDepartmentRepository.Setup(repo=> repo.GetAsync(It.IsAny(int))).Returns(Task.Run(()=>GetFakeDepartment()));
            var dController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
            var result = await dController.Get();

            var objectResult = Assert.IsType<ObjectResult>(result);
            var departments = Assert.IsType<List<Department>>(objectResult.Value);
            Assert.Equal(departments.Count, 3);
        }

        private async Task<List<Department>> GetFakeDepartment()
        {   
            return await Task.Run(()=>{
                return new List<Department>{
                    new Department{Id=0, Title="It"},
                    new Department{Id=1, Title="Marketing"},
                    new Department{Id=2, Title="Accountant Department"}
                };
            });
        }
    }
}
