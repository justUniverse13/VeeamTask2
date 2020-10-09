using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TestProjectSelenium.Code.Base;
using TestProjectSelenium.Code.Dto;
using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Extensions
{
    static class WebDriverExtentions 
    {
        private static readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(45);
        private static readonly TimeSpan PollingInterval = TimeSpan.FromSeconds(5);
        private const int WaitTimes = 8;

        public static string CreateScreenshot(this RemoteWebDriver driver, string fileName)
        {
            try
            {
                FileSystemHelper.EnsureScreensotsFolderExists();
                var filePath = FileSystemHelper.GetPathForScreenshot(fileName);
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                return filePath;
            }
            catch (Exception e)
            {
                Logger.Write($"Unable to get screenshot: {e.Message}");
                return string.Empty;
            }
        }

        public static void QuitDriver(this RemoteWebDriver driver)
        {
            try
            {
                Logger.Write("Trying to quit Browser...");
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Close();
                driver.Quit();
                driver.Dispose();
                Logger.Write("Browser closed successfully.");
            }
            catch (Exception e)
            {
                Logger.Write("Browser was not closed successfully:");
                Logger.Write(e);
            }
        }

        public static T NowAt<T>(this RemoteWebDriver driver) where T : SeleniumBasePage, new()
        {
            var page = new T { Driver = driver, Actions = new Actions(driver) };
            return page;
        }

        public static void WaitForDataLoading(this RemoteWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(400);
            WaitForLoading(driver, By.XPath("//div[@class='cg-busy-default-spinner']/following-sibling::div[text()='Please Wait...']"));
            WaitForLoading(driver, By.XPath(".//div/span[contains(@class, 'ui-select-refreshing fa fa-circle-o-notch fa-spin')]"));
        }

        private static void WaitForLoading(this RemoteWebDriver driver, By @by)
        {
            WebDriverWait wait = new WebDriverWait(driver, WaitTimeout);
            wait.Until(WebDriverWaits.InvisibilityOfElementLocated(by));
        }

        public static void WaitForElement(this RemoteWebDriver driver, By by)
        {
            var attempts = WaitTimes;
            while (attempts > 0)
            {
                try
                {
                    ExecuteWithLogging(() => FluentWait.Create(driver)
                        .WithTimeout(WaitTimeout)
                        .WithPollingInterval(PollingInterval)
                        .Until(WebDriverWaits.VisibilityOfAllElementsLocatedBy(by)), by);
                    return;
                }
                // System.InvalidOperationException : Error determining if element is displayed (UnexpectedJavaScriptError)
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Following Exception is occured in the WaitForElement method: {0}", e.Message);
                }
                // System.InvalidOperationException :The xpath expression './/option[contains(text(),'xxx')]' cannot be evaluated or does notresult in a WebElement
                catch (InvalidSelectorException e)
                {
                    Console.WriteLine("Following Exception is occured in the WaitForElement method: {0}", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error waiting element by '{by}' : {e.Message}");
                    throw;
                }
                finally
                {
                    attempts--;
                }
            }
        }

        private static void ExecuteWithLogging(Action actionToExecute, By by)
        {
            try
            {
                actionToExecute();
            }
            catch (Exception)
            {
                Logger.Write($"Error while wating for {by}");

                throw;
            }
        }

        // Sample usage
        //	Driver.WaitWhileControlIsNotDisplayed<ClaimListPage>(() => claimListPage.CreateClaimButton);
        //	OR JUST
        //	Driver.WaitWhileControlIsNotDisplayed<ClaimListPage>(() => NowHere<ClaimListPage>().CreateClaimButton);
        public static void WaitWhileControlIsNotClicable<T>(this RemoteWebDriver driver, Expression<Func<IWebElement>> elementGetter)
        {
            var propertyName = ((MemberExpression)elementGetter.Body).Member.Name;
            var by = GetByFor<T>(propertyName);
            WebDriverWait wait = new WebDriverWait(driver, WaitTimeout);
            wait.Until(WebDriverWaits.ElementToBeClickable(by));
        }

        // Sample usage
        //	Driver.WaitWhileControlIsNotDisplayed<ClaimListPage>(() => claimListPage.CreateClaimButton);
        //	OR JUST
        //	Driver.WaitWhileControlIsNotDisplayed<ClaimListPage>(() => NowHere<ClaimListPage>().CreateClaimButton);
        public static void WaitWhileControlIsNotDisplayed<T>(this RemoteWebDriver driver, Expression<Func<IWebElement>> elementGetter)
        {
            var propertyName = ((MemberExpression)elementGetter.Body).Member.Name;
            var by = GetByFor<T>(propertyName);
            driver.WaitWhileControlIsNotDisplayed(by);
        }

        public static void WaitWhileControlIsNotDisplayed(this RemoteWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, WaitTimeout);
            wait.Until(WebDriverWaits.VisibilityOfAllElementsLocatedBy(by));
        }

        public static By GetByFor<T>(string element)
        {
            var propertyName = element;
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var findsByAttributes =
                property.GetCustomAttributes(typeof(FindsByAttribute), inherit: false).Single() as FindsByAttribute;
            return ByFactory.From(findsByAttributes);
        }

        public static bool IsElementDisplayed(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementDisplayed(this RemoteWebDriver driver, By selector)
        {
            try
            {
                return driver.FindElement(selector).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static WebDriverWait Wait(this ISearchContext context, double timeout = 4.0, string message = null)
        {
            WebDriverWait webDriverWait = new WebDriverWait(context.ToDriver(), TimeSpan.FromSeconds(timeout));
            webDriverWait.Message = message;
            return webDriverWait;
        }

        public static WebDriverWait Wait(this ISearchContext context, double timeout, double sleepInterval, string message = null)
        {
            WebDriverWait webDriverWait = new WebDriverWait((IClock)new SystemClock(), context.ToDriver(), TimeSpan.FromSeconds(timeout), TimeSpan.FromSeconds(sleepInterval));
            webDriverWait.Message = message;
            return webDriverWait;
        }

        public static void RetryInCaseOfException(this RemoteWebDriver driver, Action method, int retryNumber = 8, int secondsToWaitBeforeNextRetry = 5)
        {
            for (var i = 0; i <= retryNumber; i++)
            {
                try
                {
                    method();
                }
                catch (Exception e)
                {
                    if (i != retryNumber)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondsToWaitBeforeNextRetry);
                        continue;
                    }
                    Console.WriteLine(e);
                    throw;
                }
                break;
            }
        }
    }
}
