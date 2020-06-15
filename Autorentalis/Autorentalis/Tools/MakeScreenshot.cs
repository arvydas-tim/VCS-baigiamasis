using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autorentalis.Tools
{
    public static class MakeScreenshot
    {
        public static void MakePhoto(IWebDriver webdriver)
        {
            // padarom screenshot'a teste atidaryto lango
            Screenshot myScreenshot = webdriver.TakeScreenshot();

            // gauna vykdomos programos adresa
            string screenShotDirectory =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location)));

            // sukuria naujo folderio adresa, suteikia jam pavadinima
            string sceenShotFolder = Path.Combine(screenShotDirectory, "screenshots");

            // sukuria nauja aplankala panaudodamas pries tai duota adresa
            Directory.CreateDirectory(sceenShotFolder);

            // sukuriam screenshoto failo pavadinima is testo pavadinimo ir testo atlikimo datos
            string screenShotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:HH-mm}.png";

            // sukuria screenshoto adresa, suteikia jam pavadinima
            string screenshotPath = Path.Combine(sceenShotFolder, screenShotName);

            myScreenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            Console.WriteLine($"Isssaugojom {screenshotPath}");
        }
    }
}
