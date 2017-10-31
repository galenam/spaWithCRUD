using System.Collections.Generic;
using DepartmentWebApi.code.Model;
using System.Threading.Tasks;
using System;
using BaseWebApi.Code.Interfaces;

namespace DepartmentWebApi.code.Interfaces{
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
	}
}