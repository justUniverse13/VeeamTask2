using System.Configuration;

namespace TestProjectSelenium.Code.Providers
{
    class UrlProvider
    {
        public static string StartUrl => ConfigurationManager.AppSettings["appURL"];
    }
}
