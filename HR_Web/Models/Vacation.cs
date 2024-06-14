namespace HR_Web.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }


        public Vacation()
        {
            Status = "Pending";
        }

        public Vacation(int employeeId, string employeeName, DateTime startDate, DateTime endDate, string status, string reason)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            StartDate = startDate;
            EndDate = endDate;
            Status = "Pending";
            Reason = reason;
        }

    }
}
