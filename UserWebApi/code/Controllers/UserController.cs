using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseWebApi.Code.Interfaces;
using UserWebApi.Code.Model;
using Microsoft.Extensions.Logging;
using BaseWebApi.Code.AbstractClasses;
using System.ComponentModel.DataAnnotations;

namespace UserWebApi.code.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {        
        public UserController(IBaseRepository<User> dr, ILogger<UserController> logger):
            base(dr, logger)
        {
        }
    }
}
