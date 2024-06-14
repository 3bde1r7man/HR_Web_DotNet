using HR_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HR_Web.Util;




namespace HR_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HRContext _context = new HRContext();

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

            _context.Employees.Add(employee);
            _context.SaveChanges();
            
            ViewBag.SuccessMessage = "Employee added successfully!";
            return View();
        }

        
        public IActionResult SearchEmployee(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return View("Search", _context.Employees.ToList());
            }

            IQueryable<Employee> employeesQuery = _context.Employees;
            var queryParts = searchQuery.Trim().Split();
            

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
            
          
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                ViewBag.ErrorMessage = "Employee not found.";
                return View();
            }
            ViewBag.EmployeeId = employee.Id;
            ViewBag.EmployeeName = employee.FirstName + " " + employee.LastName;
            
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
            _context.Vacations.Add(vacation);
            _context.SaveChanges();
            
            ViewBag.SuccessMessage = "Vacation added successfully!";

            return View("Search", _context.Employees.ToList());
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
            
            var vacation = _context.Vacations.Find(id);
            if (vacation == null)
            {
                ViewBag.ErrorMessage = "Vacation not found.";
                return View("Vacations", _context.Vacations.ToList());
            }
            vacation.Status = "Approved";
            _context.SaveChanges();
            
            ViewBag.SuccessMessage = "Vacation approved successfully!";
            return View("Vacations", _context.Vacations.ToList());
        }

        public IActionResult RejectVacation(int id)
        {
            using (var db = new HRContext())
            {
                var vacation = db.Vacations.Find(id);
                if (vacation == null)
                {
                    ViewBag.ErrorMessage = "Vacation not found.";
                    return View("Vacations", db.Vacations.ToList());
                }
                vacation.Status = "Rejected";
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Vacation rejected successfully!";
            return View("Vacations", _context.Vacations.ToList());
        }
        
        public IActionResult EditEmployee(int id)
        {
            using (var db = new HRContext())
            {
                var emp = db.Employees.Find(id);
                if (emp == null)
                {
                    ViewBag.ErrorMessage = "Employee not found.";
                    return View();
                }
                return View(emp);
            }
        }

        public IActionResult UpdateEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName) ||
                string.IsNullOrEmpty(employee.Email) || string.IsNullOrEmpty(employee.Phone) ||
                string.IsNullOrEmpty(employee.Marital) || string.IsNullOrEmpty(employee.Address) || employee.Salary <= 0 ||
                employee.VacationDays == 0 || employee.ApprovedVacation < 0)
            {
                ViewBag.ErrorMessage = "Please fill all fields.";
                return View("EditEmployee");
            }

            if (!new GenderValidator().ValidateGender(employee.Gender) || !new MaritalValidator().ValidateMarital(employee.Marital))
            {
                ViewBag.ErrorMessage = "Invalid Gender or Marital status.";
                return View("EditEmployee");
            }
            if (!new EmailValidator().ValidateEmail(employee.Email) || !new PhoneValidator().ValidatePhoneNumber(employee.Phone))
            {
                ViewBag.ErrorMessage = "Invalid email or phone number.";
                return View("EditEmployee");
            }

            
            var emp = _context.Employees.Find(employee.Id);
            if (emp == null)
            {
                ViewBag.ErrorMessage = "Employee not found.";
                return View("EditEmployee");
            }
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Email = employee.Email;
            emp.Phone = employee.Phone;
            emp.Marital = employee.Marital;
            emp.Address = employee.Address;
            emp.Salary = employee.Salary;
            emp.Gender = employee.Gender;
            emp.VacationDays = employee.VacationDays;
            emp.ApprovedVacation = employee.ApprovedVacation;
            _context.SaveChanges();
            
            ViewBag.SuccessMessage = "Employee updated successfully!";
            return View("Search", _context.Employees.ToList());
        }

        public IActionResult DeleteEmployee(int id)
        {
            using (var db = new HRContext())
            {
                var emp = db.Employees.Find(id);
                if (emp == null)
                {
                    ViewBag.ErrorMessage = "Employee not found.";
                    return View("EditEmployee");
                }
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
            ViewBag.SuccessMessage = "Employee deleted successfully!";
            return View("Search", _context.Employees.ToList());
        }
    }
}
