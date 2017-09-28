using System.Collections.Generic;
using code.Model;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		IEnumerable<Department> GetAll();
		Department Get(int id);
		int Insert(Department department);
		int Update(Department department);
		int Delete(int id);
	}
}