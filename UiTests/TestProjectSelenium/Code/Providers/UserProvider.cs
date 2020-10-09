using TestProjectSelenium.Code.Dto;

namespace TestProjectSelenium.Code.Providers
{
    class UserProvider
    {
        public static UserDto Client => new UserDto()
        {
            Login = "login.test@gmail.com",
            Password = "123admi!B",
            Name = "Test",
            LastName = "Test"
        };
    }
}
