using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Eclipse.UI.Tests.Integration
{
    public class ScreenshotTaker
    {
        private const string PathToScreenShots = "Screenshots\\";
        private static readonly string PathToBin = Directory
            .GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString();
        private static readonly string FullPathToScreenshots = Path.Combine(PathToBin, PathToScreenShots);

        public static void DeleteOldScreenshots()
        {
            var directory = new DirectoryInfo(FullPathToScreenshots);

            if (!directory.Exists)
            {
                return;
            }

            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        public static void TakeScreenShots(IWebDriver driver, ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError == null)
            {
                return;
            }

            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            var title = scenarioContext.ScenarioInfo.Title;
            var runname = String.Concat(title, DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));
            if (!Directory.Exists(FullPathToScreenshots))
            {
                Directory.CreateDirectory(FullPathToScreenshots);
            }

            ss.SaveAsFile($"{FullPathToScreenshots}{runname}.jpg");
        }
    }
}