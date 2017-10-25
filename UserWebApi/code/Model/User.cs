using System.ComponentModel.DataAnnotations;

namespace UserWebApi.Code.Model
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		public int DepartmentId { get; set; }
	}
}