using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace RedGate.Deploy.WebAppTests.Pages
{
    public class LogoutPage : PageBase
    {
        public LogoutPage(IWebDriver driver) : base(driver)
        {
        }

#pragma warning disable 649 //649 is warning about unassigned private members - but these are assigned by reflection
        [FindsBy(How = How.Id, Using = "logOutButton")]
        private IWebElement mLogoutButton;
#pragma warning restore 649

        public void Logout()
        {
            mLogoutButton.Submit();
        }

        public static void Logout(IWebDriver driver, string baseUrl)
        {
            NavigateWithoutAuthentication(driver, baseUrl, "accounts/logout");
            if (driver.Url.EndsWith("accounts/logout")) new LogoutPage(driver).Logout();
        }
    }
}
