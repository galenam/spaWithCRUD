using BaseWebApi.Code.Interfaces;
using UserWebApi.Code.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.Constants;

namespace UserWebApi.code.DB
{
	public class UserRepository : IBaseRepository<User>
	{
		private readonly UserDBContext _context;
        private readonly  ILogger<UserRepository> _logger;
  
        public UserRepository(UserDBContext context, ILogger<UserRepository> logger)
        {
            _context = context;      
            _logger = logger;  
        }

        public async Task<List<User>> GetAllAsync()
        {            
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorDbConnection, ex, $"Error db connection");
                return null;                
            }
        }
 
        public async Task<User> GetAsync(long id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorGettingId, ex, $"Error getting {id}");                
                return null;
            }
        }
 
        public async Task<long> InsertAsync(User User)
        {
            try{
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                return User.Id;                
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.InsertDBError, ex, $"Insert error in db User UserName={User.UserName}");
                return -1;
            }
        }

        public async Task<bool> UpdateAsync(User User)
        {            
            if (User == null) {
                _logger.LogError(LoggingEvents.EmtptyModel, $"Update error in db User UserName={User.UserName}, id={User.Id}");                
                return await Task.FromResult(false);
            }
            var UserForUpdate = await GetAsync(User.Id);
            if (UserForUpdate == null)
            {
                _logger.LogError(LoggingEvents.NoSuchIdInDB, $"Update error in db User no such id: id={User.Id}, UserName={User.UserName}");                
                return await Task.FromResult(false);
            }
            UserForUpdate.UserName = User.UserName;   
			UserForUpdate.DepartmentId = User.DepartmentId;
            var existedUserInSuchDB = _context.Users.FirstOrDefault(u => u.UserName == UserForUpdate.UserName);
            if ( existedUserInSuchDB!= null )
            {
                _logger.LogInformation(LoggingEvents.TitleExistsInDB, $"Such title exists in DB. Title={existedUserInSuchDB.UserName}, existed id={existedUserInSuchDB.Id}, updated User id={User.Id}");
                return await  Task.FromResult(false);
            }
            try
            {
                _context.Users.Update(UserForUpdate);
                return await _context.SaveChangesAsync()>0;
            }
            catch(Exception ex)
            {   
                _logger.LogInformation(LoggingEvents.UpdateDBError, ex,  $"Can't update User in DB. Title={User.UserName}, id={User.Id}");                
                return await Task.FromResult(false);
            }
        }
 
        public async Task<bool> DeleteAsync(long id)
        {            
            try
            {
                var entity = _context.Users.First(t => t.Id == id);
                if (entity == null)
                {
                    _logger.LogInformation(LoggingEvents.IdNotExists,  $"Such id not exists in DB. id={id}");                                    
                    return false;
                }
                _context.Users.Remove(entity);
                return await _context.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.DeleteDBError, ex,  $"Can't delete User in DB. id={id}");                                
                return false;
            }
        }
	}
}