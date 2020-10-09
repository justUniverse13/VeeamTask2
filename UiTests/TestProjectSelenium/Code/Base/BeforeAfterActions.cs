using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using BoDi;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TestProjectSelenium.Code.Extensions;
using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Base
{
    [Binding]
    class BeforeAfterActions : BaseTest
    {
        private readonly IObjectContainer _objectContainer;

        public BeforeAfterActions(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void OnStartUp()
        {
            var driverInstance = CreateBrowserDriver();

            #region Set Browser size

            var browserSize = ConfigurationManager.AppSettings["browserSize"];

            switch (browserSize)
            {
                case "Default":
                    break;
                case "FullScreen":
                    driverInstance.Manage().Window.Maximize();
                    break;
                default:
                    {
                        if (browserSize.Contains(','))
                            driverInstance.Manage().Window.Size = new Size(int.Parse(browserSize.Split(',')[0]), int.Parse(browserSize.Split(',')[1]));
                        else
                            throw new Exception($"Browser window size can't be set. Unknown settings: '{browserSize}'");
                    }
                    break;
            }

            #endregion

            _objectContainer.RegisterInstanceAs<RemoteWebDriver>(driverInstance);
        }

        [AfterScenario]
        public void OnTearDown()
        {
            try
            {
                var driver = _objectContainer.Resolve<RemoteWebDriver>();

                BrowserScreenshot.Get(driver, GetType().Name);

                Logger.Write($"Closing window at: {driver.Url}");

                driver.QuitDriver();
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
        }

        [AfterStep]
        public void AfterEachStep()
        {
            var driver = _objectContainer.Resolve<RemoteWebDriver>();

            driver.WaitForDataLoading();
        }
    }
}
