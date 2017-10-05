using System.Collections.Generic;
using code.Model;
using System.Threading.Tasks;
using System;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		Task<List<Department>> GetAllAsync();
		Task<Department> GetAsync(long id);
		Task<long> InsertAsync(Department department);
		Task<bool> UpdateAsync(Department department);
		Task<bool> DeleteAsync(long id);
	}
}