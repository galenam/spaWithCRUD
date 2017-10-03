using Interfaces;
using code.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    // todo : add logging
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly DepartmentDBContext _context;
  
        public DepartmentRepository(DepartmentDBContext context)
        {
            _context = context;        
        }

        public async Task<List<Department>> GetAllAsync()
        {            
            return await _context.Departments.ToListAsync();
        }
 
        public async Task<Department> GetAsync(long id)
        {
            return await _context.Departments.FirstOrDefaultAsync(t => t.Id == id);
        }
 
        public int Insert(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }
 
 // todo : return error description
        public int Update(Department department)
        {            
            if (department == null) {return -1;}
            if (_context.Departments.FirstOrDefault(d => d.Title == department.Title) == null )
            {
                return -1;
            }
            try
            {
            _context.Departments.Update(department);
            return _context.SaveChanges();
            }
            catch(Exception)
            {   
                return -1;
            }
        }
 
        public int Delete(long id)
        {
            var entity = _context.Departments.First(t => t.Id == id);
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }
	}
}