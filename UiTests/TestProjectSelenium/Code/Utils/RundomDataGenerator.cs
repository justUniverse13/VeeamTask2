using System;
using System.Linq;

namespace TestProjectSelenium.Code.Utils
{
    public class RundomDataGenerator
    {
        private static Random random = new Random();
        public static string Numeric(int length)
        {
            Random rand = new Random();
            string result = rand.Next(1, 9).ToString();

            while (result.Length < length)
            {
                result += rand.Next(9).ToString();
            }
            return result;
        }

        public static string Alphanumeric(int length)
        {
            var result = string.Empty;
            while (result.Length < length)
                result += Guid.NewGuid().ToString("N").ToUpper();

            return result.Substring(0, length);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABT";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Email()
        {
            return $"At{Alphanumeric(10)}@{Alphanumeric(5)}.com";
        }
    }
}
