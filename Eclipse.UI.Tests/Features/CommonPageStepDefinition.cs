using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Eclipse.UI.Tests.Pages;
using NUnit.Framework;
using Eclipse.UI.Tests.Integration.ExcelDataAccess.UserCredentials;

namespace Eclipse.UI.Tests.Features
{
    [Binding]
    public class CommonPageStepDefinition : BaseStep
    {
        private readonly ScenarioContext _scenarioContext;
        private Login _login;
        private DashBoard _dashBoard;

        public CommonPageStepDefinition(IWebDriver driver, ScenarioContext scenarioContext) : base(driver)
        {
            _scenarioContext = scenarioContext;
            InitializePageObjects();
        }

        [Given(@"I Login to '(.*)' application as '(.*)'")]
        public void GivenILoginToApplicationAs(string Application, string nameOfTheUser)
        {
            _login.Navigate(Application);
            BrowserAlertHandler();
            WaitForPageLoad(50);
            Assert.True(IsOneOfTheElementEnabled(_login.UserName, _dashBoard.LoggedInUserName, 50), 
                "The Eclipse Login page is NOT loaded");

            if (IsElementPresent(_dashBoard.LoggedInUserName)) return;
            var userData = UserDataAccess.GetUserCredentails(nameOfTheUser);
            _login.EnterUsername(userData.Username);
            _login.EnterPassword(userData.Password);
            _login.ClickLoginButton();

            AnotherUserDetectionDialogHandler();
            Assert.True(WaitForElement(_dashBoard.LoggedInUserName, 50), "The User Dashboard page is NOT loaded");
        }

        [Then(@"I logout from the eclipse application")]
        public void ThenILogOutFromTheEclipseApplication()
        {
            _dashBoard.ClickOnLoggedInUserName();
            WaitForElement(_dashBoard.LoggedInUserMenu, 20);
            _dashBoard.ClickLoggedInUserMenuOption("Logout");
            Assert.True(WaitForElement(_login.UserName, 50), 
                "The Eclipse Login page is NOT loaded after logout from the application");
        }

        private void AnotherUserDetectionDialogHandler()
        {
            IsOneOfTheElementEnabled(_login.AnotherLoginDetectedDialog, _dashBoard.LoggedInUserName, 20);
            if (IsElementPresent(_login.AnotherLoginDetectedDialog))
            {
                _login.ClickLoginButton();
            }
        }

        private void BrowserAlertHandler()
        {
            var MainWindow = Driver.CurrentWindowHandle;
            ImplicitWait(3);
            try
            {
                Driver.SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException)
            {
                // ignored
            }

            Driver.SwitchTo().Window(MainWindow);
        }

        private void InitializePageObjects()
        {
            _login = new Login(Driver);
            _dashBoard = new DashBoard(Driver);
        }
    }
}