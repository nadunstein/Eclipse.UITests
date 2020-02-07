using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using CompleteActivitiesPage = Eclipse.UI.Tests.Pages.WorkBench.CompleteActivities.CompleteActivities;

namespace Eclipse.UI.Tests.Features.WorkBench.CompleteActivities
{
    [Binding]
    public class FeatureSteps : BaseStep
    {
        private readonly ScenarioContext _scenarioContext;

        private CompleteActivitiesPage _completeActivities;

        public FeatureSteps(IWebDriver driver, ScenarioContext scenarioContext) : base(driver)
        {
            _scenarioContext = scenarioContext;
            InitializePageObjects();
        }

        [Then(@"I navigate to WORKBENCH-Complete Activities page")]
        public void ThenINavigateToWORKBENCH_CompleteActivitiesPage()
        {
            _completeActivities.Navigate();
            WaitForPageLoad(60);
            Assert.True(WaitForElement(_completeActivities.CompleteActivitiesPanel, 50) &&
                WaitForElement(_completeActivities.PageTitle, 50) && _completeActivities.IsPageLoaded(),
                "The WORKBENCH-Complete Activities page is NOT loaded");
        }

        [When(@"I include the reference number to the search field on Approved tab panel in WORKBENCH-Complete Activities page")]
        public void WhenIIncludeTheReferenceNumberToTheSearchFieldOnApprovedTabPanelInWORKBENCH_CompleteActivitiesPage()
        {
            Assert.True(WaitForElement(_completeActivities.ApprovedTabSearchField, 20), 
                "The search field is NOT displayed on Approved tab panel in WORKBENCH-Complete Activities page");
            string referenceNumber = _scenarioContext["ReferenceNumber"].ToString();
            _completeActivities.EnterApprovedTabSearchValue(referenceNumber);
        }

        [Then(@"I verify the approved gold loan ticket is listed in WORKBENCH-Complete Activities page")]
        public void ThenIVerifyTheApprovedGoldLoanTicketIsListedInWORKBENCH_CompleteActivitiesPage()
        {
            ImplicitWait(3);
            Assert.AreEqual(1, _completeActivities.GetApprovedActivitySearchresultCount(),
                "More than one search results are listed in WORKBENCH-Complete Activities page");
            Assert.AreEqual(_scenarioContext["ReferenceNumber"].ToString(), 
                _completeActivities.GetApprovedActivitySearchResultReferenceNumber(), 
                "The approved gold loan ticket is NOT listed in WORKBENCH-Complete Activities page");
        }

        private void InitializePageObjects()
        {
            _completeActivities = new CompleteActivitiesPage(Driver);
        }
    }
}