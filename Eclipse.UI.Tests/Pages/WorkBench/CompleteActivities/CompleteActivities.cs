using OpenQA.Selenium;
using System;
using System.Configuration;

namespace Eclipse.UI.Tests.Pages.WorkBench.CompleteActivities
{
    public class CompleteActivities
    {
        private readonly IWebDriver _driver;

        public readonly By PageTitle = By.CssSelector(".content-title"); 

        public readonly By CompleteActivitiesPanel = By.CssSelector("complete-activities");

        public readonly By ApprovedTabPanelHeader = By.CssSelector("#ui-tabpanel-10-label");

        public readonly By RejectedTabPanelHeader = By.CssSelector("#ui-tabpanel-11-label");

        public readonly By ApprovedTabSearchField = By.CssSelector("#ui-tabpanel-2 input");

        public readonly By ApprovedActivityResultGrid = By.CssSelector("#ui-tabpanel-2 .ui-table-tbody");

        public CompleteActivities(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            const string completeActivitiesUrl = "/#/workbench/complete-activities";
            _driver.Url = string.Concat(ConfigurationManager.AppSettings["WORKBENCH"], completeActivitiesUrl);
        }

        public bool IsPageLoaded()
        {
            var pageTitleValue = _driver.FindElement(PageTitle).Text;
            return pageTitleValue.Contains("Complete Activities");
        }

        public void clickApprovedTabPanelHeader()
        {
            _driver.FindElement(ApprovedTabPanelHeader).Click();
        }

        public void EnterApprovedTabSearchValue(string value)
        {
            _driver.FindElement(ApprovedTabSearchField).Click();
            _driver.FindElement(ApprovedTabSearchField).SendKeys(value);
        }

        public int GetApprovedActivitySearchresultCount()
        {
            try
            {
                var searchResults = _driver.FindElement(ApprovedActivityResultGrid).
                    FindElements(By.CssSelector("tr"));
                return searchResults.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string GetApprovedActivitySearchResultReferenceNumber()
        {
            var searchResultReferenceNumber = _driver.FindElement(ApprovedActivityResultGrid).
                FindElement(By.CssSelector("tr td"));
            return searchResultReferenceNumber.Text.Trim();
        }
    }
}