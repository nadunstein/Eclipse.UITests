using OpenQA.Selenium;

namespace Eclipse.UI.Tests.Pages
{
    public class DashBoard
    {
        private readonly IWebDriver _driver;

        public readonly By PageTitle = By.CssSelector(".content-title");

        public readonly By LoggedInUserName = By.CssSelector(".logged-user-name");

        public readonly By LoggedInUserMenu = By.CssSelector(".ng-trigger-overlayAnimation");       

        public DashBoard(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsPageLoaded()
        {
            var pageTitleValue = _driver.FindElement(PageTitle).Text;
            return pageTitleValue.Contains("Dashboard");
        }

        public void ClickOnLoggedInUserName()
        {
            _driver.FindElement(LoggedInUserName).Click();
        }

        public void ClickLoggedInUserMenuOption(string menuOption)
        {
            var options = _driver.FindElement(LoggedInUserMenu).
                FindElements(By.CssSelector(".ui-menuitem-text"));
            foreach (var option in options)
            {
                if (option.Text.Contains(menuOption))
                {
                    option.Click();
                    return;
                }
            }
        }
    }
}