using System.Collections.Generic;
using code.Model;

namespace Interfaces{
	public interface IDepartmentRepository
	{
		IEnumerable<Department> GetAll();
		Department Get(string title);
		int Insert(Department department);
		//int Update(int id, Department department);
		int Delete(string title);
	}
}