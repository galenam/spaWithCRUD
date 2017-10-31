using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BaseWebApi.Code.Interfaces;

namespace  DepartmentWebApi.code.Model
{
    public partial class Department: IModel
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
