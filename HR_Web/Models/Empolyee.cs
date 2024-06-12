namespace HR_Web.Models
{
    public class Empolyee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        
        public double salary { get; set; }
        public int vacationDays { get; set; }
        public int aprrovedVacationDays { get; set; }



        public string material { get; set; }
        public string gender { get; set; }
        public DateTime birthDate { get; set; }
        public string address { get; set; }


        public Empolyee()
        {
        }

        public Empolyee(int id, string firstName, string lastName, string email, string phone, string password, double salary, int vacationDays, int aprrovedVacationDays, string material, string gender, string dob, string address)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Password = password;
            this.salary = salary;
            this.vacationDays = vacationDays;
            this.aprrovedVacationDays = aprrovedVacationDays;
            this.material = material;
            this.gender = gender;
            this.birthDate = DateTime.Parse(dob);
            this.address = address;
        }

    }
}
