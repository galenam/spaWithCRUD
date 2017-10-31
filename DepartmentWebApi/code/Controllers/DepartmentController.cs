using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DepartmentWebApi.code.Interfaces;
using DepartmentWebApi.code.Model;
using Microsoft.Extensions.Logging;
using BaseWebApi.code.Controllers;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebApi.code.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : BaseController<Department>
    {        
        public DepartmentController(IDepartmentRepository dr, ILogger<DepartmentController> logger):
            base(dr, logger)
        {
        }
    }
}
