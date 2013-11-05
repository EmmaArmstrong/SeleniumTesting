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
            var envServer = Environment.GetEnvironmentVariable("SMOKETESTSERVER");
            SmokeTestUrlBase = string.Format("http://localhost:8080", envServer ?? "rgd-smoketest");
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            
            Driver = new FirefoxDriver();
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