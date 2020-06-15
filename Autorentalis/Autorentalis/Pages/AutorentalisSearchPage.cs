using Autorentalis.Pages.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Autorentalis.Pages
{
     public class AutorentalisSearchPage :PageBase
    {
        private IWebElement _searchBlurb => Driver.FindElement(By.Id("search_form"));
        private IWebElement _4DoorsCheckbox => Driver.FindElement(By.XPath("//label[@class='custom-control-label' and @for='doors']"));

        private IWebElement _pickFirstResultButton => Driver.FindElement(By.XPath("/html/body/section/form/div/div[3]/div/div[3]/div/div[2]/div[1]/div[3]/div[3]/div[2]/div[2]/a"));
        
            
        //private IWebElement _loadingSpinner => Driver.FindElement(By.CssSelector(".loader_ajax"));
   
        public AutorentalisSearchPage(IWebDriver webdriver) : base(webdriver)
        {

        }
        private void WaitToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".loader_ajax")));

        }


        public AutorentalisSearchPage AssertSearchPage()
        {
            WaitToLoad();


            Assert.That(_searchBlurb.Text.Contains("PAIEŠKOS REZULTATAI"), "Failed to load/recognize search page");

            return this;
        }
        public AutorentalisOrderPage ClickFirstResultPickButton()
        {
            WaitToLoad();

            _pickFirstResultButton.Click();        

            return new AutorentalisOrderPage(Driver);
        }

        public AutorentalisSearchPage Check4PlusDoorsCheckbox()
        {
            WaitToLoad();

            _4DoorsCheckbox.Click();
           
            return this;
        }
        public AutorentalisSearchPage CheckSupplierCheckbox(Tiekejas tiekejas)
        {
            WaitToLoad();
            
            int tiekejoValue = Convert.ToInt32(tiekejas);
            IWebElement _supplierCheckbox = Driver.FindElement(By.XPath($"//label[@class='custom-control-label' and @for='supplier{tiekejoValue}']"));
            _supplierCheckbox.Click();
            return this;
        }

    }

}