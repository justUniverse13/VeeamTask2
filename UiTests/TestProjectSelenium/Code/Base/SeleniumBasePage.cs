using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using TestProjectSelenium.Code.Extensions;

namespace TestProjectSelenium.Code.Base
{
    public abstract class SeleniumBasePage
    {
        public RemoteWebDriver Driver { get; set; }

        public Actions Actions { get; set; }

        public virtual List<By> GetPageIdentitySelectors()
        {
            return GetType()
                .GetProperties()
                .Select(p => ReflectionExtensions.GetFirstDecoration<FindsByAttribute>(p))
                .Where(a =>
                    ((object)a) != null
                    && a != null)
                .Select(Utils.ByFactory.From)
                .ToList();
        }

        public By SelectorFor<TPage, TProperty>(TPage page, Expression<Func<TPage, TProperty>> expression)
        {
            var attribute = Extensions.ReflectionExtensions.ResolveMember(page, expression).GetFirstDecoration<FindsByAttribute>();
            return Utils.ByFactory.From(attribute);
        }
    }
}