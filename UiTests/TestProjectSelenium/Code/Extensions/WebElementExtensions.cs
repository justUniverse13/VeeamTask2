using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using TestProjectSelenium.Code.Utils;

namespace TestProjectSelenium.Code.Extensions
{
    public static class WebElementExtensions
    {
        //This is specific method for 'ng-table-select-count' elements
        public static void SelectboxSelect(IWebElement selectbox, string option)
        {
            try
            {
                selectbox.Click();
                // wait for dropdown to appear
                selectbox.FindElement(By.XPath($"//*[@class='ui-select-choices-row-inner']/div[text()='{option}']")).Click();
            }
            catch (Exception e)
            {
                Logger.Write($"Unable to select '{option}' value in the selectbox.");
                throw e;
            }
        }

        //This is specific method for 'ng-table-select-count' elements
        public static string GetSelectedValue(IWebElement selectbox)
        {
            try
            {
                return selectbox.FindElement(By.XPath(".//div[contains(@ng-bind,'select.selected.name')]")).Text;
            }
            catch (Exception e)
            {
                Logger.Write("Unable to get selected value from selectbox");
                throw e;
            }
        }

        public static bool Displayed(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool Displayed(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static IWebElement GetDisplayedElementFromList(IList<IWebElement> list)
        {
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (!item.Displayed) continue;
                    return item;
                }
                throw new Exception("All web elements in sequence are not displayed.");
            }
            throw new Exception("List contains no elements.");
        }

        public static List<string> GetUlOptions(this IWebElement element)
        {
            try
            {
                var options = element.FindElements(By.XPath("./following-sibling::ul//div[@ng-class]//div"));

                return options.Any() ?
                    options.Select(x => x.Text).ToList() :
                    new List<string>() { element.FindElement(By.XPath("./following-sibling::ul//div[@ng-class]//span[contains(@ng-if,'notFound')]")).Text };
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (TargetInvocationException)
            {
                return null;
            }
        }

        public static void TryClick(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to click on {(object)element.ToDriver().GetType()} - {(object)ex}");
                Console.WriteLine("WARN: Going to perform JsClick");
                element.JsClick(true);
            }
        }

        #region Extension method for Switchers

        public static void Switch(this IWebElement switcher, bool condition)
        {
            if (condition != SwitcherState(switcher))
                switcher.Click();
        }

        public static bool SwitcherState(this IWebElement switcher)
        {
            return switcher.GetAttribute("class").Contains("checked");
        }

        #endregion
    }
}