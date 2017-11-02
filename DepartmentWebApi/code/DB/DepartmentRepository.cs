using DepartmentWebApi.code.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.Constants;
using BaseWebApi.Code.Interfaces;
using BaseWebApi.Code.AbstractClasses;

namespace DepartmentWebApi.code.DB
{
	public class DepartmentRepository : BaseRepository<Department>
	{

        public DepartmentRepository(DepartmentDBContext context, ILogger<DepartmentRepository> logger)
        :base(context, logger)
        {

        }

		protected override DbSet<Department> GetDbSet()
		{
			return ((DepartmentDBContext)_context).Departments;
        }

		protected override void UpdateModel(Department destination, Department source)
		{
			destination.Name = source.Name;
		}
	}
}