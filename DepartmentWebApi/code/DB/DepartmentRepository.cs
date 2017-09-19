using Interfaces;
using Models;
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
            return _context.Departments.ToList();
        }
 
        public Department Get(int id)
        {
            return _context.Departments.First(t => t.Id == id);
        }
 
        public int Insert(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }
 
        public int Update(int id, Department department)
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