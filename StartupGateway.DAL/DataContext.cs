using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StartupGateway.BusinessEntities;
using StartupGateway.Model;

//Packages used : MySql.EntityFrameworkCore(Overrides Microsoft.EntityFrameworkCore)


//Ref: https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core.html

namespace StartupGateway.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        //OnConfiguring method is an inbuilt method of MySQL EFCore
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=Local instance 3306;user=root;password=1qaz!QAZ");

        }
        //This code defines a `DataContext` class with a constructor that sets the database options, a
        //`DbSet` property for `Projects` entities, and an empty `OnModelCreating` method for model configuration.
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        //protected override void OnModel Creating(ModelBuilder modelBuilder) { }
    }
}

