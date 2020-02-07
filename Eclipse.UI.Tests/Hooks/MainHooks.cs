using System;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using Eclipse.UI.Tests.Integration;

namespace Eclipse.UI.Tests.Hooks
{
    [Binding]
    public class MainHooks
    {
        private static IWebDriver _driver;
        private readonly IObjectContainer _objectContainer;
        private static ScenarioContext _scenarioContext;
        private static readonly string ProjectPath = Directory.GetParent(
            Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString()
        ).ToString();
        private static readonly string ChromeDriverPath = string.Concat(ProjectPath, "\\Driver\\ChromeDriver");

        public MainHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun(Order = 0)]
        public static void DeleteOldScreenshots()
        {
            ScreenshotTaker.DeleteOldScreenshots();
        }

        [BeforeScenario(Order = 0)]
        public void StartBrowser()
        {
            _driver = InitializeChromeDriver();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario(Order = 0)]
        public void ScreenShotTakeforFailScenario()
        {
            ScreenshotTaker.TakeScreenShots(_driver, _scenarioContext);
        }

        [AfterScenario(Order = 1)]
        public void CloseBrowser()
        {
            QuitDriver(_driver);
        }

        [AfterTestRun]
        public static void KillChromeDriverProcess()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

            foreach (Process chromeDriverProcess in chromeDriverProcesses)
            {
                chromeDriverProcess.Kill();
            }
        }

        private static IWebDriver InitializeChromeDriver()
        {
            var options = new ChromeOptions();

            if (ConfigurationManager.AppSettings["ChromeBrowserHeadlessMode"] == "True")
            {
                options.AddArguments("headless");
            }

            options.AddArguments("window-size=1366x768");
            options.AddArguments("start-maximized"); // open Browser in maximized mode
            options.AddArguments("disable-infobars"); // disabling infobars
            options.AddArguments("--disable-extensions"); // disabling extensions
            options.AddArguments("--disable-gpu"); // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage"); // overcome limited resource problems
            options.AddArguments("--no-sandbox"); // Bypass OS security model

            var driver = new ChromeDriver(ChromeDriverPath, options, TimeSpan.FromMinutes(5));
            return driver;
        }

        private static void QuitDriver(IWebDriver driver)
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }
}