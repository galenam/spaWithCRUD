using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace  UserWebApi.Code.Model
{
    public partial class UserDBContext : DbContext
    {

        public UserDBContext(DbContextOptions options) : base(options){}
        public virtual DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Name)
                    .HasName("user_username_unique")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
