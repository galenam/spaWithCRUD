using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseWebApi.Code.Interfaces{
	public interface IBaseRepository<T>
	where T : IModel
	{		
		Task<List<T>> GetAllAsync();
		Task<T> GetAsync(long id);
		Task<long> InsertAsync(T model);
		Task<bool> UpdateAsync(T model);
		Task<bool> DeleteAsync(long id);
	} 
}