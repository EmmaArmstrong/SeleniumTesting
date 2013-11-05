using System;
using NUnit.Framework;
using RedGate.Deploy.WebAppTests.Pages;

namespace RedGate.Deploy.SmokeTests
{
    [TestFixture]
    public class VersionTest : SmokeTestBase
    {
        [Test]
        public void LoginPageShowsCurrentVersion()
        {
            Version expectedVersion = GetType().Assembly.GetName().Version;

            LoginPage loginPage = LoginPage.Load(Driver, SmokeTestUrlBase);

            Assert.AreEqual("v" + expectedVersion, loginPage.VersionNumber.Trim());
        }

        [Test]
        public void CopyrightDetailsAreCorrect()
        {
            LoginPage loginPage = LoginPage.Load(Driver, SmokeTestUrlBase);

            StringAssert.Contains("©", loginPage.CopyrightMessage);
            StringAssert.Contains("Red Gate", loginPage.CopyrightMessage);
            StringAssert.Contains(DateTime.Now.ToString("yyyy"), loginPage.CopyrightMessage);
        }
    }
}