using Autorentalis.Pages.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorentalis.Pages
{
    public class AutorentalisOrderPage  :PageBase
    {
        private IWebElement _orderBlurb => Driver.FindElement(By.CssSelector(".car-order"));
        private IWebElement _doorNumber => Driver.FindElement(By.XPath("//*[@class='car-card-item doors']"));

        private IWebElement _supplierTermsText => Driver.FindElement(By.CssSelector(".alamo-conditions"));

        
        public AutorentalisOrderPage(IWebDriver webdriver) : base(webdriver)
        {

        }
        
        private void WaitToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".loader_ajax")));

        }

        public AutorentalisOrderPage AssertDoorNumberIs4Plus()
        {
            WaitToLoad();

            Assert.That(Convert.ToInt32(_doorNumber.Text) > 3, "Door number was less than 4");
            return this;
        }


        public AutorentalisOrderPage AssertSupplier(Tiekejas tiekejas)
        {
            WaitToLoad();
            string supplierName = Convert.ToString(tiekejas);
            Assert.That(_supplierTermsText.Text.Contains(supplierName), "Supplier name does not match the requested supplier");
            return this;
        }

        public AutorentalisOrderPage AssertOrderPage()
        {
            WaitToLoad();


            Assert.That(_orderBlurb.Displayed, "Failed to load/recognize order page");

            return this;
        }
    }
}
