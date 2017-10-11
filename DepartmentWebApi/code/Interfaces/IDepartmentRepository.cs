using System.Collections.Generic;
using DepartmentWebApi.code.Model;
using System.Threading.Tasks;
using System;

namespace DepartmentWebApi.code.Interfaces{
	public interface IDepartmentRepository
	{
		Task<List<Department>> GetAllAsync();
		Task<Department> GetAsync(long id);
		Task<long> InsertAsync(Department department);
		Task<bool> UpdateAsync(Department department);
		Task<bool> DeleteAsync(long id);
	}
}