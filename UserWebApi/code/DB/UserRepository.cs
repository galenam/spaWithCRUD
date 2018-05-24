using BaseWebApi.Code.Interfaces;
using UserWebApi.Code.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.Constants;
using BaseWebApi.Code.AbstractClasses;

namespace UserWebApi.code.DB
{
	public class UserRepository : BaseRepository<User>
	{

        public UserRepository(UserDBContext context, ILogger<UserRepository> logger)
        :base(context, logger)
        {

        }

		protected override DbSet<User> GetDbSet()
		{
			return ((UserDBContext)_context).Users;
        }

		protected override void UpdateModel(User destination, User source)
		{
			destination.Name = source.Name;
			destination.DepartmentId = source.DepartmentId;
		}
	}
}