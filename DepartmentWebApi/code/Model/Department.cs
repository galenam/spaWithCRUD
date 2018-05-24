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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) 
                    return false;

            Department departmentToCompare = (Department)obj; 
            return (departmentToCompare.Id == Id) && (string.Compare(departmentToCompare.Name, Name, true) == 0);
        }

        public override int GetHashCode()
        {
            return (int)Id ^ Name.GetHashCode();
        }
	}
}
