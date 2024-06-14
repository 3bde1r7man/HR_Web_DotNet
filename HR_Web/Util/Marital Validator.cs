namespace HR_Web.Util
{
    public class MaritalValidator
    {
        public bool ValidateMarital(string marital)
        {
            /*
             ('widowed','widowed'),
        ('divorced','divorced'),
        ('separated','separated'),
             */
            if (marital != "Single" && marital != "Married" && marital != "Widowed" && marital != "Divorced" && marital != "Separated")
            {
                return false;
            }
            return true;
        }
    }
}
