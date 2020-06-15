using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Autorentalis.Pages
{
    public class AutorentalisHomePage : PageBase
    {

        private readonly string MainAdress = "https://autorentalis.lt/";
        private int todayDate = DateTime.Now.Day;

        private IWebElement _pickUpPlace => Driver.FindElement(By.Id("select2-full_pick_up_place-container"));
        private IWebElement _pickUpPlaceDropDownItem1 => Driver.FindElement(By.XPath("//*[@id='select2-full_pick_up_place-results']/li[1]"));
        
        //private IWebElement _dropOffPlace => Driver.FindElement(By.Id("select2-full_drop_off_place-container"));
            

        private IWebElement _pickUpDateButton => Driver.FindElement(By.Id("pick_up_date"));
        private IWebElement _dropOffDateButton => Driver.FindElement(By.Id("drop_off_date"));

        private IWebElement _dropOffDateCalendarArrowBack => Driver.FindElement(By.XPath("/html/body/div[7]/div[2]/table[1]/thead/tr[1]/th[1]/span"));

        private IWebElement _searchStartButton => Driver.FindElement(By.CssSelector(".search-btn"));
            
        private IWebElement _timeErrorMessage => Driver.FindElement(By.XPath(" //div[@id='ajax_errors']/div/ul/li"));

        public AutorentalisHomePage(IWebDriver webdriver) : base(webdriver)
        {

        }

        public AutorentalisHomePage GoToPage()
        {
            Driver.Url = MainAdress;
            return this;
        }

        public AutorentalisHomePage AcceptCookies()
        {
            Driver.Manage().Cookies
                .AddCookie(new Cookie("cookieBubble", "true", ".autorentalis.lt", "/", DateTime.Now.AddMonths(1)));
            Driver.Navigate().Refresh();
            return this;
        }

        public AutorentalisHomePage SelectPickUpAndDropOffDate(int pickUpDayOfTheMonth = 0, int rentTimeInDays = 7)
        {
            //sitam metodui galima duota bet kokia menesio diena, ir jis parinks ja is dropdown kalendoriaus
            //pickUpDayOfTheMonth DEFAULT reiksme bandys paimti rytoju (nuo tada kai vykdomas metodas)        
            //rytoju imame tam kad nereiketu uztikrinti laiko rodmens didesnio negu dabartis
            // rentTimeInDays tai skirtumas tarp pickup dienos ir dropoff dienos
            // jei rentTimeInDays neigiamas - intervalas apsivers, pickUpDayOfTheMonth taps atidavimo diena    
            // taip galima gauti intervala kuris prasideda anksciau nei dabartis

            int dropOffDayOfTheMonth = 0;
            string pickUpMonthClassName = "month1";
            string dropOffMonthClassName = "month1";
           

            if (pickUpDayOfTheMonth == 0)
            {
                pickUpDayOfTheMonth = todayDate +1;
                dropOffDayOfTheMonth = pickUpDayOfTheMonth + rentTimeInDays;

            }
            else
            {
                dropOffDayOfTheMonth = pickUpDayOfTheMonth + rentTimeInDays;
            }

            // sitie if tikrina ar pickUpDayOfTheMonth ir dropOffDayOfTheMonth neislips is kairiojo menesio dienu
            // jei islipa - pakeicia klases varda xpath'e, kad kreiptusi i desini menesi
            // taip pat perskaicioja dienos reiksme pagal tai kiek islipa is kairio menesio ribu
            if (pickUpDayOfTheMonth > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
            {

                pickUpDayOfTheMonth = pickUpDayOfTheMonth - DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                pickUpMonthClassName = "month2";
            }
            if (dropOffDayOfTheMonth > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
            {

                dropOffDayOfTheMonth = dropOffDayOfTheMonth - DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                dropOffMonthClassName = "month2";
            }
 


            _pickUpDateButton.Click();
            IWebElement pickUpDateNumber = Driver.FindElement(By.XPath($"//div[contains(@class, 'date-picker-wrapper single-date no-shortcuts no-gap two-months')]//table[contains(@class, '{pickUpMonthClassName}')]//div[text() ='{pickUpDayOfTheMonth}']"));
            pickUpDateNumber.Click();

            _dropOffDateButton.Click();

            //sitas if tikrina kai metodui duotas neigiamas rentTimeInDays, ar dropOffDayOfTheMonth neislipa is pirmojo menesio ribu
            // jei islipa priskirs nauja dropOffDayOfTheMonth reiksme ir paspaus mygtuka menesiui atgal
            if (dropOffDayOfTheMonth < 0)
            {
                dropOffDayOfTheMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1) + dropOffDayOfTheMonth; // praeito menesio dienus skaicius + neigiamas dienu skaicius = kelinta praeito menesio diena reikia clickint
                _dropOffDateCalendarArrowBack.Click(); //UI pereina i praieta menesi
                IWebElement dropOffDateNumber = Driver.FindElement(By.XPath($"//div[contains(@class, 'date-picker-wrapper no-shortcuts two-months has-gap')]//table[contains(@class, 'month1')]//div[contains(@class, 'day toMonth valid tmp') and text() ='{dropOffDayOfTheMonth}']"));
                dropOffDateNumber.Click();
            }
            else
            {
                IWebElement dropOffDateNumber = Driver.FindElement(By.XPath($"//div[contains(@class, 'date-picker-wrapper no-shortcuts no-gap two-months')]//table[contains(@class, '{dropOffMonthClassName}')]//div[text() ='{dropOffDayOfTheMonth}']"));
                dropOffDateNumber.Click();
            }

            return this;
        }

        public AutorentalisHomePage SetPickUpAndDropOffPlace()
        {            
            _pickUpPlace.Click();
            _pickUpPlaceDropDownItem1.Click();
            return this;
        }

        public AutorentalisSearchPage PressSearchButton()
        {
            _searchStartButton.Click();

            return new AutorentalisSearchPage(Driver);
        }

        public AutorentalisHomePage AssertTimeErrorMessage()
        {
            Assert.That(_timeErrorMessage.Text, Is.EqualTo("Klaida - neteisingai pasirinkote pasiėmimo datą arba laiką. Pasiėmimas negali būti ankstesniu laiku nei dabartis."),"Failed to find the correct error message");        
            return this;
        }

    }
}
