using System.Text.RegularExpressions;

namespace HR_Web.Util
{
    public class EmailValidator
    {
        private Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        public bool ValidateEmail(string email)
        {
            if (!regex.IsMatch(email))
            {
                return false;
            }
            return true;
        }
    }
}