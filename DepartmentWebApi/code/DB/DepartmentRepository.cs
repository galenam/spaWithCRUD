using DepartmentWebApi.code.Interfaces;
using DepartmentWebApi.code.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DepartmentWebApi.code.Constants;

namespace DepartmentWebApi.code.DB
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly DepartmentDBContext _context;
        private readonly  ILogger _logger;
  
        public DepartmentRepository(DepartmentDBContext context, ILogger logger)
        {
            _context = context;      
            _logger = logger;  
        }

        public async Task<List<Department>> GetAllAsync()
        {            
            return await _context.Departments.ToListAsync();
        }
 
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
                _logger.LogError(LoggingEvents.InsertDBError, ex, $"Insert error in db department title={department.Title}");
                return -1;
            }
        }

        public async Task<bool> UpdateAsync(Department department)
        {            
            if (department == null) {return await Task.FromResult(false);}
            var existedDepatmentInSuchDB = _context.Departments.FirstOrDefault(d => d.Title == department.Title);
            if ( existedDepatmentInSuchDB!= null )
            {
                _logger.LogInformation(LoggingEvents.TitleExistsInDB, $"Such title exists in DB. Title={existedDepatmentInSuchDB.Title}, existed id={existedDepatmentInSuchDB.Id}, updated department id={department.Id}");
                return await  Task.FromResult(false);
            }
            try
            {
                _context.Departments.Update(department);
                return await _context.SaveChangesAsync()>0;
            }
            catch(Exception ex)
            {   
                _logger.LogInformation(LoggingEvents.UpdateDBError, ex,  $"Can't update department in DB. Title={department.Title}, id={department.Id}");                
                return await Task.FromResult(false);
            }
        }
 
        public async Task<bool> DeleteAsync(long id)
        {            
            try
            {
                var entity = _context.Departments.First(t => t.Id == id);
                if (entity == null)
                {
                    _logger.LogInformation(LoggingEvents.DepartmentWithIdNotExists,  $"Such id not exists in DB. id={id}");                                    
                    return false;
                }
                _context.Departments.Remove(entity);
                return await _context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.DeleteDBError, ex,  $"Can't delete department in DB. id={id}");                                
                return false;
            }
        }
	}
}