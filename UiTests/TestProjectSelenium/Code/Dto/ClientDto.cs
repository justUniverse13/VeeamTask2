using System;
using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Dto
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }

        public string ContacnFirstName { get; set; }
        public string ContacnLastName { get; set; }
        public string ContcatPhone { get; set; }
        public string WorkEmail { get; set; }
        public string WorkPhone { get; set; }

        public ClientDto()
        {
            Id = Guid.NewGuid().ToString("N");
            Name = "Name" + RundomDataGenerator.Alphanumeric(5);

            State = RundomDataGenerator.Alphanumeric(2);
            City = "City" + RundomDataGenerator.Alphanumeric(5);
            PostalCode = RundomDataGenerator.Numeric(5).ToString();
            Address = "Address" + RundomDataGenerator.Alphanumeric(5);

            ContacnFirstName = "Fn_" + RundomDataGenerator.Alphanumeric(5);
            ContacnLastName = "Ln_" + RundomDataGenerator.Alphanumeric(5);
            ContcatPhone = RundomDataGenerator.Numeric(10).ToString();
            WorkEmail = RundomDataGenerator.Email();
            WorkPhone = RundomDataGenerator.Numeric(10).ToString();
        }

        public string ContactName()
        {
            return $"{ContacnFirstName} {ContacnLastName}";
        }

        public string FullAddress()
        {
            return $"{Address}, {City}, {State}, {PostalCode}";
        }
    }
}
