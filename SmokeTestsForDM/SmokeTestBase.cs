using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace RedGate.Deploy.SmokeTests
{
    public class SmokeTestBase
    {
        protected readonly string SmokeTestUrlBase;
        protected IWebDriver Driver;

        public SmokeTestBase()
        {
           SmokeTestUrlBase = string.Format("http://localhost:8080");
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {            
            Driver = new FirefoxDriver();
           
            /* 
             * DesiredCapabilities caps = DesiredCapabilities.InternetExplorer();
            caps.SetCapability("version", "9");
            Driver = new RemoteWebDriver(new Uri("http://selenium-hub1.testnet.red-gate.com:4444/wd/hub"), caps);
             
             */
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            using (Driver)
            {
                Driver.Quit();
            }
        }
    }
}