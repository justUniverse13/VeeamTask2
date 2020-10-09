using System;
using System.Collections.Generic;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProjectSelenium.Code.Base
{
    class BaseTest
    {
        public RemoteWebDriver Driver { get; set; }

        public RemoteWebDriver CreateBrowserDriver()
        {
            var browserType = ConfigurationManager.AppSettings["targetBrowser"];

            switch (browserType)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("no-sandbox");

                    ChromeDriver chromeDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
                    chromeDriver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
                    return chromeDriver;
                case "Firefox":
                    return new FirefoxDriver();
                case "InternetExplorer11":
                    return new InternetExplorerDriver();
                case "Edge":
                    return new EdgeDriver();
                case "Headless":
                    var driverService = ChromeDriverService.CreateDefaultService();
                    var chromeOptions = new ChromeOptions();
                    driverService.HideCommandPromptWindow = true;
                    chromeOptions.AddArgument("--headless");
                    return Driver = new ChromeDriver(driverService, chromeOptions);
                default:
                    throw new Exception($"Browser type '{browserType}' was not identified");
            }
        }
    }
}
