using OpenQA.Selenium;
using System.Configuration;

namespace Eclipse.UI.Tests.Pages
{
    public class Login
    {
        private readonly IWebDriver _driver;

        public readonly By UserName = By.CssSelector("#float-username");

        public readonly By Password = By.CssSelector("#float-password");

        public readonly By LoginButton = By.CssSelector("button");

        public readonly By AnotherLoginDetectedDialog = By.CssSelector("p-confirmdialog");

        public readonly By AnotherLoginDetectedDialogYesButton = By.CssSelector(".ui-dialog-footer button");

        public Login(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate(string ApplicationName)
        {
            _driver.Url = ConfigurationManager.AppSettings[ApplicationName];
        }

        public void EnterUsername(string userName)
        {
            _driver.FindElement(UserName).Click();
            _driver.FindElement(UserName).SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(Password).Click();
            _driver.FindElement(Password).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public void ClickYesButtonInAnotherLoginDetectedDialog()
        {
            _driver.FindElement(AnotherLoginDetectedDialogYesButton).Click();
        }
    }
}