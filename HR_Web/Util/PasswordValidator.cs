using System.Text.RegularExpressions;

namespace HR_Web.Util
{
    public class PasswordValidator
    {
        private Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        public bool StrongPassword(string password)
        {
            if (!regex.IsMatch(password))
            {
                return false;
            }
            return true;
        }
    }
}
