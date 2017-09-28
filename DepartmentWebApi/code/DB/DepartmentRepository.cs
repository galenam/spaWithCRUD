using Interfaces;
using code.Model;
using System.Collections.Generic;
using System.Linq;
namespace DB
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly DepartmentDBContext _context;
  
        public DepartmentRepository(DepartmentDBContext context)
        {
            _context = context;        
        }
 
        public IEnumerable<Department> GetAll()
        {            
            return _context.Departments;
        }
 
        public Department Get(int id)
        {
            return _context.Departments.FirstOrDefault(t => t.Id == id);
        }
 
        public int Insert(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }
 
        public int Update(Department department)
        {            
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
 
        public int Delete(int id)
        {
            var entity = _context.Departments.First(t => t.Id == id);
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }
	}
}