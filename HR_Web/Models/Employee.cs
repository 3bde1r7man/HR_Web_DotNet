namespace HR_Web.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public double Salary { get; set; }
        public int VacationDays { get; set; }
        public int ApprovedVacation{ get; set; }



        public string Marital { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }


        public Employee()
        {
        }

        public Employee(int id, string firstName, string lastName, string email, string phone, double salary, int vacationDays, int approvedVacation, string marital, string gender, string dob, string address)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Salary = salary;
            this.VacationDays = vacationDays;
            this.ApprovedVacation = approvedVacation;
            this.Marital = marital;
            this.Gender = gender;
            this.BirthDate = DateTime.Parse(dob);
            this.Address = address;
        }

    }
}
