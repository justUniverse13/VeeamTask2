using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestProjectSelenium.Code.Dto;
using TestProjectSelenium.Code.Extensions;
using TestProjectSelenium.Code.Providers;
using TestProjectSelenium.Tests.Pages;
using Xunit;

namespace TestProjectSelenium.Tests.Steps
{
    [Binding]
    class GeneralSteps : TechTalk.SpecFlow.Steps
    {

        private readonly RemoteWebDriver _driver;
        private readonly UserDto _user;

        public GeneralSteps(RemoteWebDriver driver, UserDto user)
        {
            _driver = driver;
            _user = user;
        }

        [StepDefinition(@"I open Veeam page")]
        public void IOpenVeeamPage()
        {
            try
            {
                _driver.Navigate().GoToUrl(UrlProvider.StartUrl);
                Assert.True(_driver.Url.Contains("https://careers.veeam.com/"));
            }
             catch (Exception e)
            {
                throw new Exception($"Driver can't navigate to Veeam {e.Message}.");
            }
        }

        [StepDefinition(@"I select '(.*)' item from selector by text '(.*)'")]
        public void ThenISelectItemFromSelectorById(string itemName, string text)
        {
            var generalPage = _driver.NowAt<GeneralPage>();

            void Action()
            {
                try
                {
                    generalPage.GetSelectorByText(text).Click();
                }
                catch(Exception e)
                {
                    throw new Exception($"Unable to click on {generalPage.GetSelectorByText(text)} element " + $"{e.Message}.");
                }

                try
                {
                    generalPage.GetElementFromDropdown(itemName).Click();
                }
                catch(Exception e)
                {
                    throw new Exception($"Unable to click on {generalPage.GetElementFromDropdown(itemName)} element " + $"{e.Message}.");
                }
            }
            _driver.RetryInCaseOfException(Action);
        }

        [StepDefinition(@"I check '(.*)' item by id from selector by text '(.*)'")]
        public void ICheckItemFromSelectorByText(string id, string text)
        {
            var generalPage = _driver.NowAt<GeneralPage>();

            void Action()
            {
                try
                {
                    generalPage.GetSelectorByText(text).Click();
                }
             catch(Exception e)
                {
                    throw new Exception($"Unable to click on {generalPage.GetSelectorByText(text)} element " + $"{e.Message}.");
                }

                try
                {
                    generalPage.GetCheckerById(id).Click();
                }
               catch (Exception e)
                {
                    throw new Exception($"Unable to click on { generalPage.GetCheckerById(id)} element " + $"{e.Message}.");
                }

                try
                {
                    generalPage.ApplyButton.Click();
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to click on { generalPage.GetCheckerById(id)} element " + $"{e.Message}.");
                }
            }
            _driver.RetryInCaseOfException(Action);
        }

        [StepDefinition(@"there are '(.*)' results")]
        public void ThereAreResults(string jobsCounter)
        {
            var generalPage = _driver.NowAt<GeneralPage>();

            void Action()
            {
                try
                {
                    Assert.True(generalPage.JobsCounter.Text.Contains(jobsCounter));
                }
               catch(Exception e)
                {
                    throw new Exception($"There are not {jobsCounter} jobs but " + $"{generalPage.JobsCounter.Text.ToString()}.");
                }
            }
            _driver.RetryInCaseOfException(Action);
        }

    }
}
