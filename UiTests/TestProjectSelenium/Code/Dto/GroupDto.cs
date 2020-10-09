using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Dto
{
    class GroupDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public GroupPermissionLevel PermissionLevel { get; set; }

        public GroupDto()
        {
            Name = "At_" + RundomDataGenerator.Alphanumeric(5);
            Description = "At_" + RundomDataGenerator.Alphanumeric(20);
        }
    }

    public enum GroupPermissionLevel
    {
        InvoicesOnly,
        AllButInvoices,
        AllButCollections
    }
}
