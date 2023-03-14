namespace WebAPI.Data.Models
{
    using System.Text;
    using System.Text.RegularExpressions;

    public class BulgarianPIN
    {
        private static readonly Regex RegexPhysicalPerson = new Regex(@"^\d\d[0-5]\d[0-3]\d\d{4}$");
        private static readonly int[] MultipliersPhysicalPerson = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        public static bool IsValid(string number)
        {
            number = RemoveSpecialCharacthers(number);

            return BgPhysicalPerson(number);
        }

        private static bool BgPhysicalPerson(string vat)
        {
            if (!RegexPhysicalPerson.IsMatch(vat))
            {
                return false;
            }

            var month = int.Parse(vat.Substring(2, 2));

            if ((month <= 0 || month >= 13) && (month <= 20 || month >= 33) && (month <= 40 || month >= 53))
            {
                return false;
            }

            var total = Sum(vat, MultipliersPhysicalPerson);

            total %= 11;

            if (total == 10)
            {
                total = 0;
            }

            return total == ToInt(vat[9]);
        }

        private static string RemoveSpecialCharacthers(string ssn)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ssn?.Length; i++)
            {
                if (char.IsLetterOrDigit(ssn[i]))
                {
                    sb.Append(ssn[i]);
                }
            }

            return sb.ToString();
        }

        private static int Sum(string input, int[] multipliers, int start = 0)
        {
            var sum = 0;

            for (var index = start; index < multipliers.Length; index++)
            {
                var digit = multipliers[index];
                sum += ToInt(input[index]) * digit;
            }

            return sum;
        }

        private static int ToInt(char c)
        {
            return c - '0';
        }
    }
}
