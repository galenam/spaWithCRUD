using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebApi.Code.Model;

namespace UserWebApi.Code.Interface{
	public interface IUserRepository
	{		
		Task<List<User>> GetAllAsync();
		Task<User> GetAsync(long id);
		Task<long> InsertAsync(User department);
		Task<bool> UpdateAsync(User department);
		Task<bool> DeleteAsync(long id);
	} 
}