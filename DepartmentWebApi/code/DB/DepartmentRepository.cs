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
 
 // todo : add error logging
        public async Task<Department> GetAsync(long id)
        {
            return await _context.Departments.FirstOrDefaultAsync(t => t.Id == id);
        }
 
        public async Task<long> InsertAsync(Department department)
        {
            try{
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return department.Id;                
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
 
 // todo : return error description
        public async Task<bool> UpdateAsync(Department department)
        {            
            if (department == null) {return await Task.FromResult(false);}
            if (_context.Departments.FirstOrDefault(d => d.Title == department.Title) != null )
            {
                return await  Task.FromResult(false);
            }
            try
            {
                _context.Departments.Update(department);
                return await _context.SaveChangesAsync()>0;
            }
            catch(Exception)
            {   
                return await Task.FromResult(false);
            }
        }
 
        public async Task<bool> DeleteAsync(long id)
        {            
            try
            {
                var entity = _context.Departments.First(t => t.Id == id);
                if (entity == null) {return false;}
                _context.Departments.Remove(entity);
                return await _context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
	}
}