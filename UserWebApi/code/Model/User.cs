using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseWebApi.Code.Interfaces;

namespace UserWebApi.Code.Model
{
	public class User : IModel
	{
		public long Id { get; set; }
		[Column("IdDepartment")]
		public int DepartmentId { get; set; }
		[Required]
		[Column("UserName")]
		public string Name { get; set; }		

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) 
                    return false;

            User userToCompare = (User)obj; 
            return (userToCompare.Id == Id) && (string.Compare(userToCompare.Name, Name, true) == 0) && DepartmentId == userToCompare.DepartmentId;
        }

        public override int GetHashCode()
        {
            return (int)Id ^ Name.GetHashCode() ^ DepartmentId;
        }
	}
}