using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace RedGate.Deploy.WebAppTests.Pages
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

#pragma warning disable 649 //649 is warning about unassigned private members - but these are assigned by reflection
        [FindsBy(How = How.Id, Using = "Username")]
        private IWebElement mUserNameInput;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement mPasswordInput;

        [FindsBy(How = How.Id, Using = "logInButton")]
        private IWebElement mLoginButton;
#pragma warning restore 649

        public void Login(string username, string password)
        {
            mUserNameInput.Clear();
            mUserNameInput.SendKeys(username);
            mPasswordInput.Clear();
            mPasswordInput.SendKeys(password);

            mLoginButton.Submit();
        }

        public static LoginPage Load(IWebDriver driver, string baseUrl)
        {
            if (!AmOnPage(driver))
            {
                LogoutPage.Logout(driver, baseUrl);
                if (!AmOnPage(driver))
                {
                    NavigateWithoutAuthentication(driver, baseUrl, "accounts/login");
                }
            }

            return new LoginPage(driver);
        }

        public static bool AmOnPage(IWebDriver driver)
        {
            return driver.Url.EndsWith("accounts/login");
        }
    }
}
