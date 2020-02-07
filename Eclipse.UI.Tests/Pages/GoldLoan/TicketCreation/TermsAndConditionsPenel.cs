using OpenQA.Selenium;

namespace Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation
{
    public class TermsAndConditionsPenel
    {
        private readonly IWebDriver _driver;

        public readonly By DetailsPanel = By.CssSelector("app-tc-terms-conditions");

        public readonly By DropdownItemListPanel = By.CssSelector(".ui-dropdown-items-wrapper");

        public TermsAndConditionsPenel(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickField(string fieldName)
        {
            var fields = _driver.FindElements(By.CssSelector("app-tc-terms-conditions form div span"));
            foreach (var field in fields)
            {
                if (field.Text.Contains(fieldName))
                {
                    field.Click();
                    return;
                }
            }
        }

        public void SelectDropdownItem(string dropdownItem)
        {
            var items = _driver.FindElements(By.CssSelector(".ui-dropdown-items-wrapper li"));
            foreach (var item in items)
            {
                if (item.GetAttribute("aria-label").Trim().Equals(dropdownItem.Trim()))
                {
                    item.Click();
                    return;
                }
            }
        }

        public void EnterFieldValue(string fieldName, string Value)
        {
            var fields = _driver.FindElements(By.CssSelector("app-tc-terms-conditions form div span"));
            foreach (var field in fields)
            {
                if (field.Text.Contains(fieldName))
                {
                    field.FindElement(By.TagName("input")).Click();
                    field.FindElement(By.TagName("input")).SendKeys(Value);
                    return;
                }
            }
        }
    }
}