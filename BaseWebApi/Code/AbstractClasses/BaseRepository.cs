using BaseWebApi.Code.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.Constants;

namespace BaseWebApi.Code.AbstractClasses
{
	public abstract class BaseRepository<U> : IBaseRepository<U> where U : class, IModel
	{
		protected readonly DbContext _context;
        protected readonly  ILogger<BaseRepository<U>> _logger;
  
        public BaseRepository(DbContext context, ILogger<BaseRepository<U>> logger)
        {
            _context = context;      
            _logger = logger;  
        }

		protected abstract DbSet<U> GetDbSet();
		protected abstract void UpdateModel(U destination, U source);

        public virtual async Task<List<U>> GetAllAsync()
        {            
            try
            {
                return await GetDbSet().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorDbConnection, ex, $"Error db connection");
                return null;                
            }
        }
 
        public virtual async Task<U> GetAsync(long id)
        {
            try
            {
                return await GetDbSet().FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorGettingId, ex, $"Error getting {id}");                
                return null;
            }
        }
 
        public virtual async Task<long> InsertAsync(U model)
        {
            try{
                GetDbSet().Add(model);
                await _context.SaveChangesAsync();
                return model.Id;                
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.InsertDBError, ex, $"Insert error in db department title={model.Name}");
                return -1;
            }
        }

        public virtual async Task<bool> UpdateAsync(U model)
        {            
            if (model == null) {
                _logger.LogError(LoggingEvents.EmtptyModel, $"Update error in db department title={model.Name}, id={model.Id}");                
                return await Task.FromResult(false);
            }
            var modelForUpdate = await GetAsync(model.Id);
            if (modelForUpdate == null)
            {
                _logger.LogError(LoggingEvents.NoSuchIdInDB, $"Update error in db department no such id: id={model.Id}, title={model.Name}");                
                return await Task.FromResult(false);
            }
			UpdateModel(modelForUpdate, model); 

            var existedModelInSuchDB = GetDbSet().FirstOrDefault(d => d.Name == modelForUpdate.Name);
            if ( existedModelInSuchDB!= null )
            {
                _logger.LogInformation(LoggingEvents.TitleExistsInDB, $"Such title exists in DB. Title={existedModelInSuchDB.Name}, existed id={existedModelInSuchDB.Id}, updated department id={model.Id}");
                return await  Task.FromResult(false);
            }
            try
            {
                GetDbSet().Update(modelForUpdate);
                return await _context.SaveChangesAsync()>0;
            }
            catch(Exception ex)
            {   
                _logger.LogInformation(LoggingEvents.UpdateDBError, ex,  $"Can't update department in DB. Title={model.Name}, id={model.Id}");                
                return await Task.FromResult(false);
            }
        }
 
        public virtual async Task<bool> DeleteAsync(long id)
        {            
            try
            {
                var entity = GetDbSet().First(t => t.Id == id);
                if (entity == null)
                {
                    _logger.LogInformation(LoggingEvents.IdNotExists,  $"Such id not exists in DB. id={id}");                                    
                    return false;
                }
                GetDbSet().Remove(entity);
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