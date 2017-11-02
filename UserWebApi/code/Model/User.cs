using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseWebApi.Code.Interfaces;

namespace UserWebApi.Code.Model
{
	public class User : IModel
	{
		public long Id { get; set; }
		public int DepartmentId { get; set; }
		[Required]
		[Column("UserName")]
		public string Name { get; set; }
	}
}