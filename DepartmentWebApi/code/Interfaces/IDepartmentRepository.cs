using System.Collections.Generic;
using code.Model;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		IEnumerable<Department> GetAll();
		Department Get(long id);
		int Insert(Department department);
		int Update(Department department);
		int Delete(long id);
	}
}