using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //fluent api to map book_author successfully
        //override onmodelcreate method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>()
               .HasOne(ub => ub.User)
               .WithMany(u => u.UserBooks)
               .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);
        }

        //Db Tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}
