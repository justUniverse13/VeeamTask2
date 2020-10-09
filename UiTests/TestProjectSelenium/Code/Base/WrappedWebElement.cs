using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestProjectSelenium.Code.Base
{
    public class WrappedWebElement : IWebElement, ILocatable
    {
        private IWebElement _innerWebElement;
        private By _elementSelector;
        private RemoteWebDriver _remoteWebDriver;

        public WrappedWebElement(By elementSelector, RemoteWebDriver remoteWebDriver)
        {
            _elementSelector = elementSelector;
            _remoteWebDriver = remoteWebDriver;
        }

        public WrappedWebElement(By elementSelector, RemoteWebDriver remoteWebDriver, IWebElement webElement)
            : this(elementSelector, remoteWebDriver)
        {
            _innerWebElement = webElement;
        }

        public void InitElement()
        {
            try
            {
                var wait = new WebDriverWait(_remoteWebDriver, TimeSpan.FromSeconds(15));
                wait.Until(wd => CheckElementAvailability());

                _innerWebElement = _remoteWebDriver.FindElement(_elementSelector);
            }
            catch (Exception e)
            {
                Utils.Logger.Write($"Unhandled exception occured in the 'InitElement' method: {e.Message}");
            }
        }

        private void RebindElement()
        {
            InitElement();
        }

        private T Execute<T>(Func<IWebElement, T> command)
        {
            Exception exception = null;
            for (int attempts = 5; attempts > 0; attempts--)
            {
                try
                {
                    return command(_innerWebElement);
                }
                catch (Exception e)
                {
                    RebindElement();
                    exception = e;
                }
            }

            string warningText = string.Format(@"Cannot execute command {0} for element {1}",
                ParsCommand(command.Method), _elementSelector);

            if (exception != null)
            {
                Console.WriteLine(warningText);
                throw exception;
            }
            else
                //In case command was not executed without any errors
                throw new Exception(warningText);
        }

        private void Execute(Action<IWebElement> command)
        {
            Exception exception = null;
            for (int attempts = 5; attempts > 0; attempts--)
            {
                try
                {
                    command(_innerWebElement);
                    Utils.Logger.Write($"{ParsCommand(command.Method)} {_elementSelector}");
                    return;
                }
                catch (Exception e)
                {
                    Utils.Logger.Write($"Unhandled exeception was occurred during executing {ParsCommand(command.Method)} for {_elementSelector}: {e.Message}");
                    RebindElement();
                    exception = e;
                }
            }
            Utils.Logger.Write($"Cannot execute command {ParsCommand(command.Method)} for element {_elementSelector}");
            throw exception;
        }

        private string ParsCommand(System.Reflection.MethodInfo command)
        {
            string pattern = @"\<(.*?)\>";
            var value = Regex.Match(command.ToString(), pattern).Groups[1].Value;
            return value;
        }

        private object CheckElementAvailability()
        {
            try
            {
                var elements = _elementSelector.FindElements(_remoteWebDriver);
                return elements.SingleOrDefault(e => e.Displayed) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Interface methods IWebElement

        public IWebElement FindElement(By @by)
        {
            return Execute(w => w.FindElement(@by));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return Execute(w => w.FindElements(@by));
        }

        public void Clear()
        {
            Execute(w => w.Clear());
        }

        public void SendKeys(string text)
        {
            Execute(w => w.SendKeys(text));
            // TODO Include this log item to the details log
            Console.WriteLine(@" > '{0}'", text);
        }

        public void Submit()
        {
            Execute(w => w.Submit());
        }

        public void Click()
        {
            Execute(w => w.Click());
        }

        public string GetAttribute(string attributeName)
        {
            return Execute(w => w.GetAttribute(attributeName));
        }

        public string GetProperty(string propertyName)
        {
            return Execute(w => w.GetProperty(propertyName));
        }

	    public string GetCssValue(string propertyName)
        {
            return Execute(w => w.GetCssValue(propertyName));
        }

        public string TagName
        {
            get { return Execute(w => w.TagName); }
        }

        public string Text
        {
            get { return Execute(w => w.Text); }
        }

        public bool Enabled
        {
            get { return Execute(w => w.Enabled); }
        }

        public bool Selected
        {
            get { return Execute(w => w.Selected); }
        }

        public Point Location
        {
            get { return Execute(w => w.Location); }
        }

        public Size Size
        {
            get { return Execute(w => w.Size); }
        }

        public bool Displayed
        {
            get { return Execute(w => w.Displayed); }
        }

        #endregion

        #region Interface methods ILocatable

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get { return ((ILocatable)_innerWebElement).LocationOnScreenOnceScrolledIntoView; }
            private set { }
        }

        public ICoordinates Coordinates
        {
            get { return ((ILocatable)_innerWebElement).Coordinates; }
            private set { }
        }

        #endregion
    }
}
