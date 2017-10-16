using System;
using Xunit;
using Moq;
using DepartmentWebApi.code.Interfaces;

namespace DepartmentWebApi.Tests
{
    public class TestDepartmentController
    {
        //https://metanit.com/sharp/aspnet5/22.4.php
        [Fact]
        public void TestGetAll()
        {
            var moq = new Moq<IDepartmentRepository>();
            moq.Setup(repo=> repo.GetAllAsync()).Returns(GetFakeDepartment());
        }
    }
}
