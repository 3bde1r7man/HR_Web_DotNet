using HR_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HR_Web.Util;




namespace HR_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) || string.IsNullOrEmpty(employee.Email) || string.IsNullOrEmpty(employee.Phone))
            {
                ViewBag.ErrorMessage = "Please fill all fields.";
                return View();
            }
            if (!new EmailValidator().ValidateEmail(employee.Email) || !new PhoneValidator().ValidatePhoneNumber(employee.Phone))
            {
                ViewBag.ErrorMessage = "Invalid email or phone number.";
                return View();
            }

            using (var db = new HRContext())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            return View();
        }

        
        public IActionResult SearchEmployee(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return View("Search", Enumerable.Empty<Employee>());
            }

            var queryParts = searchQuery.Trim().Split();
            IQueryable<Employee> employeesQuery = new HRContext().Employees;

            if (queryParts.Length == 2)
            {
                var firstName = queryParts[0];
                var lastName = queryParts[1];
                employeesQuery = employeesQuery.Where(e =>
                    e.FirstName.Contains(firstName) &&
                    e.LastName.Contains(lastName)
                );
            }
            else
            {
                employeesQuery = employeesQuery.Where(e =>
                    e.FirstName.Contains(searchQuery) ||
                    e.LastName.Contains(searchQuery)
                );
            }

            var employees = employeesQuery.ToList();
            return View("Search", employees);
        }

    }
}
