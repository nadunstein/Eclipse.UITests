using OpenQA.Selenium;

namespace Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation
{
    public class ValidateCustomerPanel
    {
        private readonly IWebDriver _driver;

        public readonly By NicField = By.CssSelector("app-tc-validate-customer .ui-inputgroup input");

        public readonly By SearchButton = By.CssSelector("app-tc-validate-customer .ui-inputgroup button");

        public readonly By DetailsPanel = By.CssSelector("app-tc-validate-customer");

        public ValidateCustomerPanel(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterNicNumber(string NicNumber)
        {
            _driver.FindElement(NicField).SendKeys(NicNumber);
        }

        public void ClickSearchButton()
        {
            _driver.FindElement(SearchButton).Click();
        }
    }
}