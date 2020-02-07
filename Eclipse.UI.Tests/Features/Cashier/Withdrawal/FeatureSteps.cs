using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WithdrawalPage = Eclipse.UI.Tests.Pages.Cashier.Withdrawal.Withdrawal;

namespace Eclipse.UI.Tests.Features.Cashier.Withdrawal
{
    [Binding]
    public class FeatureSteps : BaseStep
    {
        private readonly ScenarioContext _scenarioContext;
        private WithdrawalPage _withdrawal;

        public FeatureSteps(IWebDriver driver, ScenarioContext scenarioContext) : base(driver)
        {
            _scenarioContext = scenarioContext;
            InitializePageObjects();
        }

        [Then(@"I navigate to Withdrawal page")]
        public void ThenINavigateToWithdrawalPage()
        {
            _withdrawal.Navigate();
            WaitForPageLoad(60);
            Assert.True(WaitForElement(_withdrawal.SearchArea, 50), "The Withdrawal page is NOT loaded");
        }

        [Then(@"I include Reference Number to the Facility No field in Withdrawal page")]
        public void ThenIIncludeReferenceNumberToTheFacilityNoFieldInWithdrawalPage()
        {
            Assert.True(WaitForElement(_withdrawal.FacilityNoField, 50), 
                "The Reference No field is NOT displayed in Withdrawal page");
            var referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            _withdrawal.EnterFacilityNumberToFacilityNoField(referenceNumber);
        }

        [When(@"I click on Search button in Withdrawal page")]
        public void WhenIClickOnSearchButtonInWithdrawalPage()
        {
            _withdrawal.ClickSearchButton();
        }

        [Then(@"The Ticket details are displayed in Withdrawal page")]
        public void ThenTheTicketDetailsAreDisplayedInWithdrawalPage()
        {
            Assert.True(WaitForElement(_withdrawal.TicketDetailsPanel, 20), 
                "The Ticket details are NOT displayed in Withdrawal page");

            _scenarioContext["AdvanceOutstandingAmount"] = _withdrawal.GetAdvanceOutstandingValue();
            _scenarioContext["CashInHandAmount"] = _withdrawal.GetWithdrawalValue();
        }

        [Then(@"I verify Receipt Type field value is '(.*)' on Denominations panel in Withdrawal page")]
        public void ThenIVerifyReceiptTypeFieldValueIsOnDenominationsPanelInWithdrawalPage(string ReceiptType)
        {
            var receiptTypeFieldValue = _withdrawal.GetReceiptTypeFieldValue();
            Assert.AreEqual(ReceiptType, receiptTypeFieldValue, 
                "Receipt Type field value different on Denominations panel in Withdrawal page");
        }

        [Then(@"I add denominations equal to the total of paying amount on Denominations panel in Withdrawal page")]
        public void ThenIAddDenominationsEqualToTheTotalOfPayingAmountOnDenominationsPanelInWithdrawalPage()
        {
            var payingAmount = double.Parse(_withdrawal.GetWithdrawalValue());
            CountAndAddDenominations(payingAmount);
        }

        [When(@"I click on Save button on Denominations panel in Withdrawal page")]
        public void WhenIClickOnSaveButtonOnDenominationsPanelInWithdrawalPage()
        {
            _withdrawal.ClickSaveButton();
        }

        [Then(@"The Paying Successful notification is displayed to the user")]
        public void ThenThePayingSuccessfulNotificationIsDisplayedToTheUser()
        {
            Assert.True(WaitForElement(_withdrawal.SuccessNotification, 50) &&
               _withdrawal.IsSuccessNotificationDisplayed(),
               "The Paying Successful notification is NOT displayed to the user");
        }

        private void CountAndAddDenominations(double amount)
        {
            int[] notes = { 5000, 1000, 500, 100, 50, 20, 10 };
            int temp = Convert.ToInt32(amount);
            double centsAmmount = amount - temp;

            for (int i = 0; i < 7; i++)
            {
                int noOfNotes = temp / notes[i];
                if (noOfNotes == 0) continue;
                _withdrawal.AddDenominations(notes[i].ToString(), noOfNotes.ToString());
                temp = temp % notes[i];
                ImplicitWait(1);
            }

            _withdrawal.AddDenominations("COINS", (temp + centsAmmount).ToString());
            ImplicitWait(1);
        }

        private void InitializePageObjects()
        {
            _withdrawal = new WithdrawalPage(Driver);
        }
    }
}