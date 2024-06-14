﻿
using Microsoft.EntityFrameworkCore;


namespace HR_Web.Models
{
    public class HRContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vacation> Vacations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectRootDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string databaseDirectory = System.IO.Path.Combine(projectRootDirectory, "db");

            // Ensure the 'dp' directory exists
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }

            string databasePath = System.IO.Path.Combine(databaseDirectory, "EmployeeDatabase.db");
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
    }
}

