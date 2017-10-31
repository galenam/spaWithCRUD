using System.ComponentModel.DataAnnotations;
using BaseWebApi.Code.Interfaces;

namespace UserWebApi.Code.Model
{
	public class User : IModel
	{
		public long Id { get; set; }
		[Required]
		public string UserName { get; set; }
		public int DepartmentId { get; set; }
	}
}