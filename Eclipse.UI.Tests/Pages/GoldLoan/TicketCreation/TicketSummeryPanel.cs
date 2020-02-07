using OpenQA.Selenium;

namespace Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation
{
    public class TicketSummeryPanel
    {
        private readonly IWebDriver _driver;

        public readonly By DetailsPanel = By.CssSelector("app-tc-ticket-summary");

        public readonly By DoneButton = By.CssSelector(".card-footer button");

        public readonly By PrintButton = By.CssSelector("app-ticket-creation .content button");

        public TicketSummeryPanel(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickDoneButton()
        {
            _driver.FindElement(DoneButton).Click();
        }

        public void ClickPrintButton()
        {
            _driver.FindElement(PrintButton).Click();
        }
    }
}