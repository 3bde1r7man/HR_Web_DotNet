using System.Text.RegularExpressions;

namespace HR_Web.Util
{
    public class PhoneValidator
    {
        private Regex regex = new Regex(@"^01[0-2|5]\d{8}$");
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            if (!regex.IsMatch(phoneNumber))
            {
                return false;
            }
            return true;
        }
    }
}
