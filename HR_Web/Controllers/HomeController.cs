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
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) || 
                string.IsNullOrEmpty(employee.Email) || string.IsNullOrEmpty(employee.Phone) ||
                string.IsNullOrEmpty(employee.Marital) || string.IsNullOrEmpty(employee.Gender) ||
                string.IsNullOrEmpty(employee.Address) || employee.Salary <= 0 ||
                employee.VacationDays == 0 || employee.ApprovedVacation < 0)
            {
                ViewBag.ErrorMessage = "Please fill all fields.";
                return View();
            }
            if(!new GenderValidator().ValidateGender(employee.Gender) || !new MaritalValidator().ValidateMarital(employee.Marital))
            {
                ViewBag.ErrorMessage = "Invalid Gender or Marital status.";
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
            ViewBag.SuccessMessage = "Employee added successfully!";
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

        public IActionResult VacationForm(int id)
        {
            // get the employee by id and send the emp name and id to the view
            using (var db = new HRContext())
            {
                var employee = db.Employees.Find(id);
                if (employee == null)
                {
                    ViewBag.ErrorMessage = "Employee not found.";
                    return View();
                }
                ViewBag.EmployeeId = employee.Id;
                ViewBag.EmployeeName = employee.FirstName + " " + employee.LastName;
            }
            return View(); 
        }


        [HttpPost]
        public IActionResult VacationForm(Vacation vacation)
        {
            

            if (vacation.EmployeeId == 0 || vacation.StartDate == DateTime.MinValue || vacation.EndDate == DateTime.MinValue)
            {
                ViewBag.ErrorMessage = "Please fill all fields.";
                return View();
            }
            if (vacation.StartDate >= vacation.EndDate)
            {
                ViewBag.ErrorMessage = "Start date must be before end date.";
                return View();
            }
            using (var db = new HRContext())
            {
                db.Vacations.Add(vacation);
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Vacation added successfully!";
            // return to the search page
            return Redirect("SearchEmployee");
        }

        public IActionResult Vacations()
        {
            using (var db = new HRContext())
            {
                var vacations = db.Vacations.ToList();
                return View(vacations);
            }
        }


        public IActionResult ApproveVacation(int id)
        {
            using (var db = new HRContext())
            {
                var vacation = db.Vacations.Find(id);
                if (vacation == null)
                {
                    ViewBag.ErrorMessage = "Vacation not found.";
                    return Redirect("Vacations");
                }
                vacation.Status = "Approved";
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Vacation approved successfully!";
            return Redirect("/home/Vacations");
        }

        public IActionResult RejectVacation(int id)
        {
            using (var db = new HRContext())
            {
                var vacation = db.Vacations.Find(id);
                if (vacation == null)
                {
                    ViewBag.ErrorMessage = "Vacation not found.";
                    return Redirect("Vacations");
                }
                vacation.Status = "Rejected";
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Vacation rejected successfully!";
            return Redirect("/home/Vacations");
        }
    }
}
