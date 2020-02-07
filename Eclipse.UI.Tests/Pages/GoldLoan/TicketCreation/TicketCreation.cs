using OpenQA.Selenium;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation
{
    public class TicketCreation
    {
        private readonly IWebDriver _driver;

        public readonly By PageTitle = By.CssSelector(".content-title");

        public readonly By TicketCreationPanel = By.CssSelector(".content");

        public readonly By NextButton = By.CssSelector(".card-footer .btn-primary");

        public readonly By SuccessNotification = By.CssSelector(".ui-growl-message-success .ui-growl-message p");

        public readonly By ApproveNotification = By.CssSelector(".ui-growl-message-info .ui-growl-message p");

        public readonly By WorkflowStartedNotification = By.CssSelector(".ui-growl-message-success .ui-growl-message p");

        public TicketCreation(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            const string ticketCreationPageUrl = "/#/ticket-creation";
            _driver.Url = string.Concat(ConfigurationManager.AppSettings["GOLDLOAN"], ticketCreationPageUrl);
        }

        public bool IsPageLoaded()
        {
            var pageTitleValue = _driver.FindElement(PageTitle).Text;
            return pageTitleValue.Contains("Ticket Creation");
        }

        public void ClickNextButton()
        {
            _driver.FindElement(NextButton).Click();
        }

        public bool IsSuccessNotificationDisplayed()
        {
            const string expectedMessageContent = "Ticket Created Successfully";
            var ActualMessageContent = _driver.FindElement(SuccessNotification).Text;
            return ActualMessageContent.Contains(expectedMessageContent);
        }

        public bool IsApproveNotificationDisplayed()
        {
            const string expectedMessageContent = "Your ticket was approved";
            var ActualMessageContent = _driver.FindElement(ApproveNotification).Text;
            var formattedMessage = Regex.Replace(Regex.Replace(ActualMessageContent, 
                @"[\d-]", string.Empty), @"\s+", " "); 
            return formattedMessage.Contains(expectedMessageContent);
        }

        public string GetReferenceNumberFromApproveNotification()
        {
            var MessageContent = _driver.FindElement(ApproveNotification).Text;
            var referenceNumber = Regex.Match(MessageContent, @"\d+").Value;
            return referenceNumber;
        }

        public bool IsWorkflowStartedNotificationDisplayed()
        {
            const string expectedMessageContent = "Notification sent to cashier";
            var ActualMessageContent = _driver.FindElement(WorkflowStartedNotification).Text;
            return ActualMessageContent.Contains(expectedMessageContent);
        }
    }
}