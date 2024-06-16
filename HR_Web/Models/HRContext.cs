
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

        public override int SaveChanges()
        {
            var modifiedEmployees = ChangeTracker.Entries<Employee>()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in modifiedEmployees)
            {
                var employee = entry.Entity;
                var originalFirstName = entry.OriginalValues.GetValue<string>("FirstName");
                var originalLastName = entry.OriginalValues.GetValue<string>("LastName");

                if (employee.FirstName != originalFirstName || employee.LastName != originalLastName)
                {
                    UpdateVacationEmployeeName(employee.Id, employee.FirstName, employee.LastName);
                }
            }

            return base.SaveChanges();
        }

        private void UpdateVacationEmployeeName(int employeeId, string newFirstName, string newLastName)
        {
            var vacations = Vacations.Where(v => v.EmployeeId == employeeId).ToList();

            foreach (var vacation in vacations)
            {
                vacation.EmployeeName = $"{newFirstName} {newLastName}";
            }

            base.SaveChanges();
        }
    }

}

