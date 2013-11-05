using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace RedGate.Deploy.WebAppTests.Utilities.Selenium
{
    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities) : base(remoteAddress, desiredCapabilities)
        {
        }

        public Screenshot GetScreenshot()
        {
            string base64EncodedScreenshot = Execute(DriverCommand.Screenshot, null).Value.ToString();
            return new Screenshot(base64EncodedScreenshot);
        }
    }
}