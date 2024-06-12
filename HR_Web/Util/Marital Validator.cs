namespace HR_Web.Util
{
    public class MaritalValidator
    {
        public bool ValidateMarital(string marital)
        {
            if (marital != "Single" && marital != "Married")
            {
                return false;
            }
            return true;
        }
    }
}
