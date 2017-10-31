using System.Collections.Generic;
using System.Threading.Tasks;
using BaseWebApi.Code.Interfaces;
using UserWebApi.Code.Model;

namespace UserWebApi.Code.Interface{
	public interface IUserRepository:IBaseRepository<User>
	{		
	} 
}