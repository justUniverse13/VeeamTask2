using System;
using OpenQA.Selenium.Remote;
using TestProjectSelenium.Code.Extensions;

namespace TestProjectSelenium.Code.Utils
{
    class BrowserScreenshot
    {
        public static void Get(RemoteWebDriver driver, string testType)
        {
            try
            {
                //This was used for nUnit. There are no suchc functionality in xUnit
                    var testName = testType;
                    var filePath = driver.CreateScreenshot(testName);
                    Logger.Write($"Check screenshot by following path: {filePath}");
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
        }
    }
}
