using Autorentalis.Pages.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Autorentalis.Tests
{
    class AutorentalisTests :TestBase
    {
               

        [Test]
        public static void PartialSearchSmokeTest()
        {
            _autorentalisHomePage
                .GoToPage()
                .SetPickUpAndDropOffPlace()
                .SelectPickUpAndDropOffDate()
                .PressSearchButton()
                .AssertSearchPage();
        }
        [Test]
        public static void FullSmokeTest()
        {
            _autorentalisHomePage
                .GoToPage()
                .SetPickUpAndDropOffPlace()
                .SelectPickUpAndDropOffDate()
                .PressSearchButton()
                .ClickFirstResultPickButton()
                .AssertOrderPage();
        }

        [TestCase(0, -7)]
        [TestCase(0, -14)]
        [TestCase(1, -21)]
        public static void CalendarPastDateErrorMessageTest(int pickupdate, int dropoffdate)
        {
            _autorentalisHomePage
                .GoToPage()
                .SetPickUpAndDropOffPlace()
                .SelectPickUpAndDropOffDate(pickupdate, dropoffdate)
                .PressSearchButton();
            _autorentalisHomePage
                .AssertTimeErrorMessage();

        }
        [Test]
        public static void FourDoorSearchFilterTest()
        {
            _autorentalisHomePage
                .GoToPage()
                .SetPickUpAndDropOffPlace()
                .SelectPickUpAndDropOffDate()
                .PressSearchButton()
                .Check4PlusDoorsCheckbox()
                .ClickFirstResultPickButton()
                .AssertDoorNumberIs4Plus();
            
        }
        [TestCase(Tiekejas.Alamo)]
        [TestCase(Tiekejas.Autobanga)]
        [TestCase(Tiekejas.CARSrent)]
        public static void SupplierSearchFilterTest(Tiekejas tiekejas)
        {
            _autorentalisHomePage
                .GoToPage()
                .SetPickUpAndDropOffPlace()
                .SelectPickUpAndDropOffDate()
                .PressSearchButton()
                .CheckSupplierCheckbox(tiekejas)
                .ClickFirstResultPickButton()
                .AssertOrderPage();
         
        }
    }
}
