using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Eclipse.UI.Tests.Features
{
    [Binding]
    public class BaseStep
    {
        protected readonly IWebDriver Driver;

        public BaseStep(IWebDriver driver)
        {
            Driver = driver;
        }

        public void ImplicitWait(double timeoutInSeconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public void WaitForPageLoad(int timeoutInSeconds)
        {
            var pageLoaded = false;
            for (var i = 0; i <= timeoutInSeconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                if (!Driver.ExecuteJavaScript<string>("return document.readyState").Equals("complete"))
                {
                    continue;
                }

                pageLoaded = true;
                break;
            }

            if (!pageLoaded)
            {
                throw new Exception($"Page is not loaded within {timeoutInSeconds} seconds");
            }
        }

        public bool WaitForElement(By element, int timeoutInSeconds)
        {
            var elementDisplayed = false;
            ImplicitWait(1);

            while (timeoutInSeconds > 0 && elementDisplayed == false)
            {
                try
                {
                    elementDisplayed = Driver.FindElement(element).Displayed;
                }
                catch (Exception)
                {
                    ImplicitWait(1);
                    elementDisplayed = false;
                }

                timeoutInSeconds--;
            }

            return elementDisplayed;
        }

        public bool IsOneOfTheElementEnabled(By elementOne, By elementTwo, int timeoutInSeconds)
        {           
            while (timeoutInSeconds > 0)
            {
                if (IsElementPresent(elementOne) || IsElementPresent(elementTwo))
                {
                    return true;
                }

                timeoutInSeconds--;
                ImplicitWait(1);
            }

            return false;
        }

        public bool WaitUntilElementInvisible(By element, int timeoutInSeconds)
        {
            for (var second = 0; ; second++)
            {
                ImplicitWait(1);
                if (second == timeoutInSeconds) return false;
                try
                {
                    if (Driver.FindElement(element).Displayed) continue;
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }

        public void WaitElementToBeClickable(By element, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool IsElementPresent(By element)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            try
            {
                return Driver.FindElement(element).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsElementEnabled(By element)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            try
            {
                return Driver.FindElement(element).Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsElementFocused(By element)
        {
            return Driver.FindElement(element).Equals(Driver.SwitchTo().ActiveElement());
        }

        public void WaitAndSwitchToIframe(By iframeByValue)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(iframeByValue));
        }

        public void ScrollToTheElement(By element)
        {
            var webElement = Driver.FindElement(element);
            var js = Driver as IJavaScriptExecutor;
            js?.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
        }

        public void OpenAndSwitchToNewBrowserTab()
        {
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.open();");
            var newBrowserTab = Driver.WindowHandles[1];
            Driver.SwitchTo().Window(newBrowserTab);
        }

        public void RefreshWebPage()
        {
            Driver.Navigate().Refresh();
        }

        public void ClickEnterKey()
        {
            Actions builder = new Actions(Driver);
            builder.SendKeys(Keys.Enter);
        }
    }
}