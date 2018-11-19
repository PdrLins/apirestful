using Microsoft.EntityFrameworkCore;
using DesafioPitang.Models;

namespace DesafioPitang.Data
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
            modelBuilder.Entity<User>().Property(p=> p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasMany(c => c.Phones).WithOne(e => e.User);

            //UserContact
            modelBuilder.Entity<UserContact>().HasKey(e => e.Id);
            modelBuilder.Entity<UserContact>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserContact>().HasOne(e => e.User).WithMany(e => e.Phones).IsRequired();


        }

    }
}
