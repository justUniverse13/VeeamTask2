using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace TestProjectSelenium.Code.Extensions
{
    public static class JsExtensions
    {
        public static IWebElement JsHighlight(this IWebElement element, string style = "solid", string cssColor = "blue", int frameSize = 2)
        {
            element.JsFocus();
            element.ExecuteScript($"arguments[0].style.border='{(object)frameSize}px {(object)style} {(object)cssColor}'", (object)element);
            return element;
        }

        public static IEnumerable<IWebElement> JsHighlight(this IEnumerable<IWebElement> elements, string cssColor = "blue", int frameSize = 3)
        {
            List<IWebElement> list = elements.ToList<IWebElement>();
            foreach (IWebElement element in list)
                element.JsHighlight("dotted", cssColor, frameSize);
            return (IEnumerable<IWebElement>)list;
        }

        public static void JsDehighlight(this IWebElement element)
        {
            element.ExecuteScript("arguments[0].style.border='0px solid white'", (object)element);
        }

        public static IWebElement JsFocus(this IWebElement element)
        {
            element.ExecuteScript("var vph = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);var elTop = arguments[0].getBoundingClientRect().top;window.scrollBy(0, elTop-(vph/2));", (object)element);
            return element;
        }

        public static void JsClick(this IWebElement element, bool highlight = true)
        {
            element.JsFocus();
            if (highlight)
                element.JsHighlight();
            element.ExecuteScript("arguments[0].click();", (object)element);
        }

        public static void JsMouseOver(this IWebElement element)
        {
            element.JsHighlight();
            element.ExecuteScript("var element = arguments[0];var mouseEventObj = document.createEvent('MouseEvents');mouseEventObj.initEvent( 'mouseover', true, true );element.dispatchEvent(mouseEventObj);", (object)element);
        }

        public static bool IsImageLoaded(IWebElement image)
        {
            bool flag = false;
            object obj = image.ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", (object)image);
            if (obj is bool)
                flag = (bool)obj;
            return flag;
        }

        public static void JsScrollByHeight(IWebDriver driver, int pixels)
        {
            driver.ExecuteScript($"window.scroll(0, {(object)pixels});");
        }

        public static void JsScrollBy(this IWebDriver driver, int pixels)
        {
            driver.ExecuteScript($"window.scrollBy(0, {(object)pixels})", (object)"");
        }

        public static void JsScrollToEnd(this IWebDriver driver)
        {
            driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void JsScrollToTop(this IWebDriver driver)
        {
            driver.ExecuteScript("window.scrollTo(0, 0)");
        }

        public static IDictionary<string, object> JsGetAttributes(this IWebElement element)
        {
            return (IDictionary<string, object>)element.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", (object)element);
        }


        public static IWebDriver ToDriver(this ISearchContext context)
        {
            IWrapsDriver wrapsDriver = context as IWrapsDriver;
            if (wrapsDriver == null)
            {
                try
                {
                    return (IWebDriver)context;
                }
                catch (InvalidCastException)
                {
                    FieldInfo field = context.GetType().GetField("underlyingElement", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field == (FieldInfo)null)
                        return (IWebDriver)null;
                    wrapsDriver = field.GetValue((object)context) as IWrapsDriver;
                    if (wrapsDriver == null)
                        throw new ArgumentException("Element must wrap a web driver", nameof(context));
                }
            }
            return wrapsDriver.WrappedDriver;
        }

        private static IJavaScriptExecutor JsExecutor(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        public static object ExecuteScript(this ISearchContext context, string script, params object[] args)
        {
            return context.ToDriver().JsExecutor().ExecuteScript(script, args);
        }

        public static object ExecuteAsyncScript(this ISearchContext context, string script, params object[] args)
        {
            return context.ToDriver().JsExecutor().ExecuteAsyncScript(script, args);
        }
    }
}