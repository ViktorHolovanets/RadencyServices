using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace RadencyWebApplication.Models.Db
{
    public class ApiContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BooksDb");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
