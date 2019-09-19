using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment4
{
    public class LibraryContext:DbContext
    {
        private string _connectionString;

            public LibraryContext()
            {
                _connectionString = "Server=DESKTOP-1CNTA40;Database = Assignment4;User Id=zahid;Password=123;";
            }
            public LibraryContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }

        protected override void OnModelCreating(ModelBuilder builder)
        {
/*            builder.Entity<Book>(user =>
            {
                user.HasIndex(x => x.BarCode).IsUnique(true);
            });*/

            builder.Entity<IssueBook>()
                .HasKey(ib => new { ib.StudentId, ib.bookId});

            builder.Entity<IssueBook>()
                .HasOne(ib => ib.Student)
                .WithMany(i => i.BookIssues)
                .HasForeignKey(ib => ib.StudentId);

            builder.Entity<ReturnBook>()
               .HasKey(rb => new { rb.StudentId, rb.BookId });

            builder.Entity<ReturnBook>()
                .HasOne(rb => rb.Student)
                .WithMany(rb => rb.BookReturns);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<IssueBook> IssueBook { get; set; }
        public DbSet<ReturnBook> ReturnBook { get; set; }
    }
}


