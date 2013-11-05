using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace RedGate.Deploy.WebAppTests.Utilities.Selenium
{
    public static class WebDriverExtensions
    {
        public static object ExecuteJavascript(this IWebDriver driver, string script)
        {
            var executor = (IJavaScriptExecutor)driver;
            return executor.ExecuteScript(script);
        }
    }
}
