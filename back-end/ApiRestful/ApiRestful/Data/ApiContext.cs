using Microsoft.EntityFrameworkCore;
using ApiRestful.Models;

namespace ApiRestful.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasMany(c => c.Phones).WithOne();

            //UserContact
            modelBuilder.Entity<UserContact>().HasKey(e => e.Id);
            modelBuilder.Entity<UserContact>().Property(p => p.Id).ValueGeneratedOnAdd();


        }
  
    }
}
