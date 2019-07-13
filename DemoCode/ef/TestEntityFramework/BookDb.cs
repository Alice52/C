using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TestEntityFramework
{
    class BookDb : DbContext
    {
        //public DbSet<Book> Books { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=myDataBase;User Id=sa;Password=1252068782; ");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        //public DbSet<Book> Books { get; set; }
        //public DbSet<BookReview> Reviews { get; set; }
    }
}
