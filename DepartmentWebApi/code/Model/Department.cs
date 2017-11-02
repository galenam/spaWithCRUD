using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseWebApi.Code.Interfaces;

namespace  DepartmentWebApi.code.Model
{
    public partial class Department: IModel
    {
        public long Id { get; set; }
        [Required]       
        [Column("Title")]
		public string Name { get; set; }
	}
}
