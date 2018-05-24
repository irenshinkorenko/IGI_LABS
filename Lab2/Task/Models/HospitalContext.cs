using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Models
{
    public class HospitalContext: DbContext 
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();

            // set path to current directory
            builder.SetBasePath(Directory.GetCurrentDirectory());

            // get configuration from file appsettings.json
            builder.AddJsonFile("appsettings.json");

            // create configuration
            var config = builder.Build();

            string sqlConnectionString = config.GetConnectionString("SqlConnection");

            var options = optionsBuilder
                .UseSqlServer(sqlConnectionString)
                .Options;
        }
    }
}
