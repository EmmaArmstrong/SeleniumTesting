using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RedGate.Deploy.WebAppTests.Utilities;
using RedGate.Deploy.WebAppTests.Utilities.Selenium;

namespace RedGate.Deploy.WebAppTests.Pages
{
    public class PageBase
    {
        protected IWebDriver Driver;
#pragma warning disable 649 //649 is warning about unassigned private members - but these are assigned by reflection
        [FindsBy(How = How.Id, Using = "footer_version")] private IWebElement version;
        [FindsBy(How = How.Id, Using = "footer_copyright")] private IWebElement copyright;
        [FindsBy(How = How.Id, Using = "footer_edition")] private IWebElement edition;
        [FindsBy(How = How.ClassName, Using = "field-validation-error")] private IWebElement validationError;
        [FindsBy(How = How.Id, Using = "flashes")] private IWebElement flashes;
#pragma warning restore 649

        protected PageBase(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        protected static void NavigateWithAuthentication(IWebDriver driver, string baseUrl, string pageUrl)
        {
            NavigateWithAuthentication(driver, baseUrl, pageUrl, SiteUser.DefaultAdminUser);
        }

        protected static void NavigateWithAuthentication(IWebDriver driver, string baseUrl, string pageUrl, SiteUser user)
        {
            var fullUrl = new Uri(new Uri(baseUrl), pageUrl);
            driver.Navigate().GoToUrl(fullUrl);

            if(LoginPage.AmOnPage(driver))
            {
                new LoginPage(driver).Login(user.UserName, user.Password);
                driver.Navigate().GoToUrl(fullUrl);
            }
            else if(new PageBase(driver).CurrentUser != user.UserName)
            {
                LoginPage.Load(driver, baseUrl).Login(user.UserName, user.Password);
                driver.Navigate().GoToUrl(fullUrl);
            }
        }

        protected static void NavigateWithoutAuthentication(IWebDriver driver, string baseUrl, string pageUrl)
        {
            var fullUrl = new Uri(new Uri(baseUrl), pageUrl);
            driver.Navigate().GoToUrl(fullUrl);
        }

        protected string CurrentUser
        {
            get { return Driver.ExecuteJavascript("return $('#user_name').text();").ToString().Trim(); }
        }

        public string VersionNumber
        {
            get { return version.Text; }
        }

        public string Edition
        {
            get { return edition.Text; }
        }

        public string CopyrightMessage
        {
            get { return copyright.Text; }
        }

        public string ValidationError
        {
            get { return validationError.Text; }
        }

        public bool IsErrorPage
        {
            get
            {
                var breadcrumbs = Driver.FindElements(By.Id("errorMessage"));
                return breadcrumbs.Any();
            }
        }

        public bool Is403
        {
            get
            {
                var errorCode = Driver.FindElements(By.ClassName("error-code"));
                return errorCode.Any() && errorCode.Single().Text.StartsWith("Error 403:");
            }
        }

        public bool HasFlash
        {
            get { return flashes.FindElements(By.XPath("./div")).Any(); }
        }

        public IList<string> FlashMessages
        {
            get { return flashes.FindElements(By.XPath("./div/span")).Select(x => x.Text.Trim()).ToList(); }
        }

        protected bool WaitForNoElement(Func<IWebDriver, IWebElement> searchFunc)
        {
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            const int timeout = 60000; // milliseconds
            const int interval = 500; // milliseconds

            bool retval = false;

            for(int i = 0; i < timeout / interval; i++)
            {
                try
                {
                    searchFunc(Driver);
                }
                catch(NoSuchElementException)
                {
                    retval = true;
                    break;
                }
                Thread.Sleep(interval);
            }
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            return retval;
        }

        protected IWebElement FindJQueryButton(string text)
        {
            var buttonSets = Driver.FindElements(By.ClassName("ui-dialog-buttonset"));
            var buttons = buttonSets.SelectMany(x => x.FindElements(By.TagName("button")));
            return buttons.Single(x => x.Text == text);
        }
    }
}