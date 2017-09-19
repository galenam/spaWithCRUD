using Microsoft.EntityFrameworkCore;
using Models;
namespace DB
{
	public class DepartmentDBContext : DbContext
	{
		public DepartmentDBContext(DbContextOptions<DepartmentDBContext> options) :base(options)
        { }
         
        public DbSet<Department> Departments { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            builder.Entity<Department>().HasKey(m => m.Id); 
            base.OnModelCreating(builder); 
        } 
	}
}