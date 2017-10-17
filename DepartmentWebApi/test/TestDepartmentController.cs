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
        //https://metanit.com/sharp/aspnet5/22.4.php
        [Fact]
        public async void TestGetAll()
        {
            var mockDepartmentRepository = new Mock<IDepartmentRepository>();
            var mockLogger = new Mock<ILogger<DepartmentController>>();
            mockDepartmentRepository.Setup(repo=> repo.GetAllAsync()).Returns(Task.Run(()=>GetFakeDepartment()));
            var dController = new DepartmentController(mockDepartmentRepository.Object, mockLogger.Object);
            var result = await dController.Get();
            var vResult = result as ViewResult;
            /*
             // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
             */
        }

        private async Task<List<Department>> GetFakeDepartment()
        {   
            return await Task.Run(()=>{
                return new List<Department>{
                    new Department{Id=0, Title="It"},
                    new Department{Id=1, Title="Marketing"},
                    new Department{Id=0, Title="Accountant Department"}
                };
            });
        }
    }
}
