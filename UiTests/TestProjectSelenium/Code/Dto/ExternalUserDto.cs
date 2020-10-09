using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Dto
{
    class ExternalUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserPermissionLevel PermissionLevel { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string FullName()
        {
            var fullName = $"{FirstName} {LastName}";
            return string.IsNullOrEmpty(fullName) ? "No Name" : fullName;
        }

        public ExternalUserDto()
        {
            Email = RundomDataGenerator.Email();
            Question = RundomDataGenerator.Alphanumeric(5);
            Answer = RundomDataGenerator.Alphanumeric(5);
        }
    }

    public enum UserPermissionLevel
    {
        Manager,
        Viewer
    }
}
