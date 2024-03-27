/**
 * Created by: Zaid And Zuhri
 * Created on: 19/03/2024
 * Description: Data context layer
 * */

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
        public DbSet<User> Users { get; set; }    

    }



}

