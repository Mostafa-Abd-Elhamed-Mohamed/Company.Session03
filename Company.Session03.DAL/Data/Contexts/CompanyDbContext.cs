﻿using Company.Session03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Session03.DAL.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {

        public CompanyDbContext() : base() 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database = CompanySession03; Trusted_Connection = True ; TrustServerCertificate = True");
        }


        public DbSet<Department> Departments { get; set; }

    }
}
