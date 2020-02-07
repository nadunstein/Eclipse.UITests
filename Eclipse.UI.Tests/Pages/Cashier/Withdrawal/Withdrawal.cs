using OpenQA.Selenium;
using System.Configuration;

namespace Eclipse.UI.Tests.Pages.Cashier.Withdrawal
{
    public class Withdrawal
    {
        private readonly IWebDriver _driver;

        public readonly By SearchArea = By.CssSelector("eclipse-paying");

        public readonly By FacilityNoField = By.CssSelector(".ui-float-label input");

        public readonly By SearchButton = By.CssSelector("eclipse-paying button");

        public readonly By TicketDetailsPanel = By.CssSelector(".ui-card-content");

        public readonly By ReceiptTypeField = By.CssSelector(".data-panel p-dropdown input");

        public readonly By WithdrawalValueField = By.CssSelector("#withdrawAmount");

        public readonly By SaveButton = By.CssSelector(".data-panel .ui-button-info");

        public readonly By SuccessNotification = By.CssSelector(".ui-growl-message-success .ui-growl-message p");

        public Withdrawal(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            const string withdrawalUrl = "/#/cashier/pay";
            _driver.Url = string.Concat(ConfigurationManager.AppSettings["CASHIER"], withdrawalUrl);
        }

        public void EnterFacilityNumberToFacilityNoField(string facilityNumber)
        {
            _driver.FindElement(FacilityNoField).SendKeys(facilityNumber);
        }

        public void ClickSearchButton()
        {
            _driver.FindElement(SearchButton).Click();
        }

        public string GetAdvanceOutstandingValue()
        {
            var detailBoxes = _driver.FindElements(TicketDetailsPanel);
            foreach (var detailBox in detailBoxes)
            {
                if (detailBox.Text.Contains("Ticket Details"))
                {
                    var amount =  detailBox.FindElement(By.CssSelector("h4")).Text;
                    return amount.Replace(",", "");
                }
            }

            return null;
        }

        public string GetWithdrawalValue()
        {
            var amount = _driver.FindElement(WithdrawalValueField).GetAttribute("data");
            return amount.Replace(",", "");
        }

        public string GetReceiptTypeFieldValue()
        {
            return _driver.FindElement(ReceiptTypeField).GetAttribute("aria-label");
        }

        public void AddDenominations(string note, string noOfNotes)
        {
            _driver.FindElement(By.Id(note)).SendKeys(noOfNotes);
        }

        public void ClickSaveButton()
        {
            _driver.FindElement(SaveButton).Click();
        }

        public bool IsSuccessNotificationDisplayed()
        {
            const string expectedMessageContent = "Paying Successfull!";
            var ActualMessageContent = _driver.FindElement(SuccessNotification).Text;
            return ActualMessageContent.Contains(expectedMessageContent);
        }
    }
}