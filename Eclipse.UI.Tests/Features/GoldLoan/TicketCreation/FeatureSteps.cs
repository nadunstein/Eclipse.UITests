using Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TicketCreationPage = Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation.TicketCreation;

namespace Eclipse.UI.Tests.Features.GoldLoan.TicketCreation
{
    [Binding]
    public class FeatureSteps : BaseStep
    {
        private readonly ScenarioContext _scenarioContext;

        private TicketCreationPage _ticketCreation;
        private ValidateCustomerPanel _validateCustomerPanel;
        private ArticleDetailsPanel _articleDetailsPanel;
        private TermsAndConditionsPenel _termsAndConditionsPenel;
        private TicketSummeryPanel _ticketSummeryPanel;

        public FeatureSteps(IWebDriver driver, ScenarioContext scenarioContext) : base(driver)
        {
            _scenarioContext = scenarioContext;
            InitializePageObjects();
        }

        [Then(@"I navigate to GOLD LOAN-Ticket Creation page")]
        public void ThenINavigateToGOLDLOAN_TicketCreationPage()
        {
            _ticketCreation.Navigate();
            WaitForPageLoad(60);
            Assert.True(WaitForElement(_ticketCreation.TicketCreationPanel, 50) && 
                WaitForElement(_ticketCreation.PageTitle, 50) && _ticketCreation.IsPageLoaded(), 
                "The GOLD LOAN-Ticket Creation page is NOT loaded");
        }

        [Then(@"I include the customer NIC number to the NIC field on Customer Details panel in GOLD LOAN-Ticket Creation page")]
        public void ThenIIncludeTheCustomerNICNumberToTheNICFieldOnCustomerDetailsPanelInGOLDLOAN_TicketCreationPage()
        {
            var customerNicNumber = Data.GoldLoan.GetGoldLoanIsBranchCustomerNicNumber();
            _validateCustomerPanel.EnterNicNumber(customerNicNumber);
        }

        [When(@"I click on the Search button on Customer Details panel in GOLD LOAN-Ticket Creation page")]
        public void WhenIClickOnTheSearchButtonOnCustomerDetailsPanelInGOLDLOAN_TicketCreationPage()
        {
            _validateCustomerPanel.ClickSearchButton();
        }

        [Then(@"The Customer Details are displayed on Validate Customer panel in GOLD LOAN-Ticket Creation page")]
        public void ThenTheCustomerDetailsAreDisplayedOnValidateCustomerPanelInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_validateCustomerPanel.DetailsPanel, 20), 
                "The Customer Details are NOT displayed on Validate Customer panel in GOLD LOAN-Ticket Creation page");
        }

        [When(@"I click on the Next button in GOLD LOAN-Ticket Creation page")]
        public void WhenIClickOnTheNextButtonInGOLDLOAN_TicketCreationPage()
        {
            _ticketCreation.ClickNextButton();
        }

        [Then(@"The Article Details panel is displayed in GOLD LOAN-Ticket Creation page")]
        public void ThenTheArticleDetailsPanelIsDisplayedInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_articleDetailsPanel.DetailsPanel, 20),
                "The Article Details panel is NOT displayed in GOLD LOAN-Ticket Creation page");
        }

        [Then(@"I include the details on Article Details panel in GOLD LOAN-Ticket Creation page as follows:")]
        public void ThenIIncludeTheDetailsOnArticleDetailsPanelInGOLDLOAN_TicketCreationPageAsFollows(Table table)
        {
            var fields = table.CreateSet<(string FieldName, string FieldType, string FieldValues)>();
            foreach (var field in fields)
            {
                FillValuesToFields(field.FieldType, _articleDetailsPanel.ClickField, _articleDetailsPanel.EnterFieldValue, 
                    field.FieldName, _articleDetailsPanel.DropdownItemListPanel, _articleDetailsPanel.MultiSelectionDropdownItemListPanel, 
                    field.FieldValues);
            }
        }

        [When(@"I click on the Add button on Article Details panel in GOLD LOAN-Ticket Creation page")]
        public void WhenIClickOnTheAddButtonOnArticleDetailsPanelInGOLDLOAN_TicketCreationPage()
        {
            _articleDetailsPanel.ClickAddButton();
        }

        [Then(@"The Article is added to the Article Details section on Article Details panel in GOLD LOAN-Ticket Creation page")]
        public void ThenTheArticleIsAddedToTheArticleDetailsSectionOnArticleDetailsPanelInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_articleDetailsPanel.ArticalDetailsTable, 20), 
                "The Article is NOT added to the Article Details section on Article Details panel in GOLD LOAN-Ticket Creation page");
        }

        [Then(@"The Terms & Conditions panel is displayed in GOLD LOAN-Ticket Creation page")]
        public void ThenTheTermsConditionsPanelIsDisplayedInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_termsAndConditionsPenel.DetailsPanel, 20),
                "The Terms & Conditions panel is NOT displayed in GOLD LOAN-Ticket Creation page");
        }

        [Then(@"I include the details on Terms & Conditions panel in GOLD LOAN-Ticket Creation page as follows:")]
        public void ThenIIncludeTheDetailsOnTermsConditionsPanelInGOLDLOAN_TicketCreationPageAsFollows(Table table)
        {
            var fields = table.CreateSet<(string FieldName, string FieldType, string FieldValues)>();
            foreach (var field in fields)
            {
                FillValuesToFields(field.FieldType, _termsAndConditionsPenel.ClickField, _termsAndConditionsPenel.EnterFieldValue,
                    field.FieldName, _termsAndConditionsPenel.DropdownItemListPanel, null, field.FieldValues);
            }
        }

        [Then(@"The Ticket Summery panel is displayed in GOLD LOAN-Ticket Creation page")]
        public void ThenTheTicketSummeryPanelIsDisplayedInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_ticketSummeryPanel.DetailsPanel, 20),
                 "The Ticket Summery panel is NOT displayed in GOLD LOAN-Ticket Creation page");
        }

        [When(@"I click on Done button in GOLD LOAN-Ticket Creation page")]
        public void WhenIClickOnDoneButtonInGOLDLOAN_TicketCreationPage()
        {
            _ticketSummeryPanel.ClickDoneButton();
        }

        [Then(@"The Ticket Created Successfully notification is displayed to the user")]
        public void ThenTheTicketCreatedSuccessfullyNotificationIsDisplayedToTheUser()
        {
            Assert.True(WaitForElement(_ticketCreation.SuccessNotification, 20) &&
                _ticketCreation.IsSuccessNotificationDisplayed(),
                "The Ticket Created Successfully notification is NOT displayed to the user");
        }

        [Then(@"The Ticket was approved notification is displayed to the user")]
        public void ThenTheTicketWasApprovedNotificationIsDisplayedToTheUser()
        {
            Assert.True(WaitForElement(_ticketCreation.ApproveNotification, 20) && 
                _ticketCreation.IsApproveNotificationDisplayed(), 
                "The Ticket was approved notification is NOT displayed to the user");

            _scenarioContext["ReferenceNumber"] = _ticketCreation.GetReferenceNumberFromApproveNotification();
            WaitUntilElementInvisible(_ticketCreation.ApproveNotification, 20);
        }

        [When(@"I Click on the Print button in GOLD LOAN-Ticket Creation page")]
        public void WhenIClickOnThePrintButtonInGOLDLOAN_TicketCreationPage()
        {
            Assert.True(WaitForElement(_ticketSummeryPanel.PrintButton, 20), 
                "The print button is not displayed in GOLD LOAN-Ticket Creation page");
            _ticketSummeryPanel.ClickPrintButton();
        }

        [Then(@"The Work flow has started notification is displayed to the user")]
        public void ThenTheWorkFlowHasStartedNotificationIsDisplayedToTheUser()
        {
            Assert.True(WaitForElement(_ticketCreation.WorkflowStartedNotification, 20) &&
                _ticketCreation.IsWorkflowStartedNotificationDisplayed(),
                "The Work flow has started notification is NOT displayed to the user");
        }

        private void SelectDropdownItem(Action<string> ClickFieldMethod, string dropdownFieldName, By dropdownItemListPanel, 
            string itemToBeSelected)
        {
            ClickFieldMethod(dropdownFieldName);
            Assert.True(WaitForElement(dropdownItemListPanel, 20), "The dropdown item list is not displayed");
            _articleDetailsPanel.SelectDropdownItem(itemToBeSelected);
        }

        private void SelectMultiSelectionDropdownItems(Action<string> ClickFieldMethod, string dropdownFieldName, 
            By MultiSelectionDropdownItemListPanel, string dropdownValues)
        {
            ClickFieldMethod(dropdownFieldName);
            Assert.True(WaitForElement(MultiSelectionDropdownItemListPanel, 20), "The dropdown item list is not displayed");

            var itemsToBeSelected = dropdownValues.Split(',').ToList();
            foreach (var itemToBeSelected in itemsToBeSelected)
            {
                _articleDetailsPanel.SelectMultiSelectionDropdownItem(itemToBeSelected);
            }           
        }

        private void EnterTextFieldValue(Action<string, string> ClickFieldMethod, string fieldName, string fieldValue)
        {
            ClickFieldMethod(fieldName, fieldValue);
        }

        private void FillValuesToFields(string fieldType, Action<string> ClickFieldMethod, Action<string, string> EnterFieldValueMethod, 
            string dropdownFieldName, By dropdownItemListPanel, By MultiSelectionDropdownItemListPanel, string Values)
        {
            switch (fieldType)
            {
                case "Dropdown":
                    SelectDropdownItem(ClickFieldMethod, dropdownFieldName, dropdownItemListPanel, Values);
                    break;

                case "MultiSelectionDropdown":
                    SelectMultiSelectionDropdownItems(ClickFieldMethod, dropdownFieldName, MultiSelectionDropdownItemListPanel, Values);
                    break;

                case "TextField":
                    EnterTextFieldValue(EnterFieldValueMethod, dropdownFieldName, Values);
                    break;
            }

            ImplicitWait(1);
        }

        private void InitializePageObjects()
        {
            _ticketCreation = new TicketCreationPage(Driver);
            _validateCustomerPanel = new ValidateCustomerPanel(Driver);
            _articleDetailsPanel = new ArticleDetailsPanel(Driver);
            _termsAndConditionsPenel = new TermsAndConditionsPenel(Driver);
            _ticketSummeryPanel = new TicketSummeryPanel(Driver);
        }
    }
}