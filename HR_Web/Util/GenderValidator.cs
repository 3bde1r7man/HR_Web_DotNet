namespace HR_Web.Util
{
    public class GenderValidator
    {
        public bool ValidateGender(string gender)
        {
            if(gender != "Male" && gender != "Female")
            {
                return false;
            }
            return true;
        }
    }
}
