using System.Collections.Generic;
using Models;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		IEnumerable<Department> GetAll();
		Department Get(int id);
		int Insert(Department department);
		int Update(int id, Department department);
		int Delete(int id);
	}
}