using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace code.Model
{
    public partial class DepartmentDBContext : DbContext
    {

        public DepartmentDBContext(DbContextOptions options) : base(options){}
        public virtual DbSet<Department> Departments { get; set; }
// добавить unique constraint на поле title
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).IsRequired();
            });
        }
    }
}
