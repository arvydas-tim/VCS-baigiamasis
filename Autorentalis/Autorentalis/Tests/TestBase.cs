using Autorentalis.Drivers;
using Autorentalis.Pages;
using Autorentalis.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorentalis.Tests
{
    public class TestBase
    {
        public static IWebDriver _driver;
        public static AutorentalisHomePage _autorentalisHomePage;

        [OneTimeSetUp]
        public static void SetUpFirefox()
        {
            _driver = CustomDrivers.GetFireFoxDriver();

            _autorentalisHomePage = new AutorentalisHomePage(_driver);
            _autorentalisHomePage.GoToPage().AcceptCookies();

        }


        [TearDown]
        public static void SingleTestTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {                
                MakeScreenshot.MakePhoto(_driver);
            }
        }

        [OneTimeTearDown]
        public static void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
