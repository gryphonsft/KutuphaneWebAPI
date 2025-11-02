using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Role;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Borrow> Borrow { get; set; }
        public DbSet<Logs> Logs { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookCopy>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookCopies)
            .HasForeignKey(bc => bc.BookId)
            .OnDelete(DeleteBehavior.Cascade);  

            builder.Entity<Borrow>()
                .HasOne(b => b.BookCopy)
                .WithMany(book => book.Borrows)
                .HasForeignKey(b => b.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Borrow>()
                .HasOne(b => b.AppUser)
                .WithMany(user => user.Borrows)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
