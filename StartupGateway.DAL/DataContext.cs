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
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        //protected override void OnModel Creating(ModelBuilder modelBuilder) { }
    }
}

