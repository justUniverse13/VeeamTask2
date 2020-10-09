using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSelenium.Code.Base;

namespace TestProjectSelenium.Tests.Pages
{
    class GeneralPage : SeleniumBasePage
    {
        public IWebElement ApplyButton => Driver.FindElement(By.XPath("//a[@class='selecter-fieldset-submit'][contains(text(),'Apply')]"));
        public IWebElement JobsCounter => Driver.FindElement(By.XPath("//h3[@class='pb15-sm-down mb30-md-up text-center-md-down']"));

        public IWebElement GetSelectorByText(string text)
        {
            return Driver.FindElement(By.XPath($"//*[@class='selecter-selected'][contains(text(),'{text}')]"));
        }

        public IWebElement GetElementFromDropdown(string element)
        {
            return Driver.FindElement(By.XPath($"//*[@class='scroller-content']/span[@data-value='{element}']"));
        }

        public IWebElement GetCheckerById(string id)
        {
            return Driver.FindElement(By.XPath($"//div[@class='container']/div[@class='row']/fieldset/label[@for='{id}']"));
        }


    }
    
}
