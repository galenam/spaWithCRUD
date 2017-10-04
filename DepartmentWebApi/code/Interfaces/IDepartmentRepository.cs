using System.Collections.Generic;
using code.Model;
using System.Threading.Tasks;
using System;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		Task<List<Department>> GetAllAsync();
		Task<Department> GetAsync(long id);
		Task<int> Insert(Department department);
		int Update(Department department);
		int Delete(long id);
	}
}