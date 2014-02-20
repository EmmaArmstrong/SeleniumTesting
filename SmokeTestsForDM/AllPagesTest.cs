using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using RedGate.Deploy.WebAppTests.Pages;

namespace RedGate.Deploy.SmokeTests
{
    [TestFixture("AdminUser", true)]
    [TestFixture("NonAdmin", false)]
    public class AllPagesTest : SmokeTestBase
    {
        private readonly string username;
        private readonly bool isAdmin;

        public AllPagesTest(string username, bool isAdmin)
        {
            this.username = username;
            this.isAdmin = isAdmin;
        }

        [TestCase("environments")]
        [TestCase("projects")]
        [TestCase("projects/edit")]
        [TestCase("tasks")]
        [TestCase("downloads")]
        [TestCase("")]
        public void UnRestrictedPages(string url)
        {
            NavigateWithAuthentication(SmokeTestUrlBase, url);
            Assert.False(IsErrorPage(), "Got an unexpected Exception on page {0}", url);
            Assert.False(IsNotAuthorized(), "Could not access page {0}", url);
            StringAssert.DoesNotContain(";", Driver.FindElement(By.XPath("/html/body")).Text);
        }

        [TestCase("configuration")]
        [TestCase("configuration/feeds")]
        [TestCase("configuration/feeds/edit")]
        [TestCase("configuration/users")]
        [TestCase("configuration/users/edit")]
        [TestCase("configuration/backup")]
        [TestCase("configuration/certificates")]
        [TestCase("configuration/licenses")]
        [TestCase("configuration/updates")]
        public void RestrictedPages(string url)
        {
            NavigateWithAuthentication(SmokeTestUrlBase, url);
            Assert.False(IsErrorPage(), "Got an unexpected Exception on page {0}", url);
            Assert.AreEqual(!isAdmin, IsNotAuthorized(), "Access rights to page {0} are incorrect", url);
            StringAssert.DoesNotContain(";", Driver.FindElement(By.XPath("/html/body")).Text);
        }

        [TestCase("configuration/permissions")]
        [TestCase("configuration/permissions/test")]
        [TestCase("configuration/permissions/edit")]
        [TestCase("configuration/downloads")]
        public void RemovedPages(string url)
        {
            NavigateWithAuthentication(SmokeTestUrlBase, url);
            Assert.True(Is404(), "The page at {0} was supposed to be deleted", url);
        }

        [TestCase("dashboard/goboom")]
        public void TestErrorPage(string url)
        {
            NavigateWithAuthentication(SmokeTestUrlBase, url);
            Assert.True(IsErrorPage(), "Should have had an Exception on page {0}", url);
        }

        private bool IsErrorPage()
        {
            var breadcrumbs = Driver.FindElements(By.Id("errorMessage"));
            return breadcrumbs.Any();
        }

        public bool IsNotAuthorized()
        {
            var errorCode = Driver.FindElements(By.ClassName("error-code"));
            return errorCode.Any() && errorCode.Single().Text.StartsWith("Error 403:");
        }

        public bool Is404()
        {
            var errorCode = Driver.FindElements(By.ClassName("error-code"));
            return errorCode.Any() && errorCode.Single().Text.StartsWith("Error 404:");
        }

        private void NavigateWithAuthentication(string baseUrl, string pageUrl)
        {
            Uri fullUrl = new Uri(new Uri(baseUrl), pageUrl);
            Driver.Navigate().GoToUrl(fullUrl);

            if (LoginPage.AmOnPage(Driver))
            {
                new LoginPage(Driver).Login(username, "Password");
                Driver.Navigate().GoToUrl(fullUrl);
            }
            else if(!Is404() && !IsErrorPage() && Driver.FindElement(By.Id("user_name")).Text != username)
            {
                LoginPage.Load(Driver, baseUrl).Login(username, "Password");
                Driver.Navigate().GoToUrl(fullUrl);
            }
        }
    }
}
