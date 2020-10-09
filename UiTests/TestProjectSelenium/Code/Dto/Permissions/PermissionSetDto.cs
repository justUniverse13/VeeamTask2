using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Dto.Permissions
{
    class PermissionSetDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public PermissionSettings PermissionSettings { get; set; }

        public PermissionSetDto()
        {
            Name = "At_" + RundomDataGenerator.Alphanumeric(5);
            Description = "At_" + RundomDataGenerator.Alphanumeric(20);
        }
    }
}
