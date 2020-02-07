using OpenQA.Selenium;

namespace Eclipse.UI.Tests.Pages.GoldLoan.TicketCreation
{
    public class ArticleDetailsPanel
    {
        private readonly IWebDriver _driver;

        public readonly By DetailsPanel = By.CssSelector("app-tc-article-details");

        public readonly By DropdownItemListPanel = By.CssSelector(".ui-dropdown-items-wrapper");

        public readonly By MultiSelectionDropdownItemListPanel = By.CssSelector(".ui-multiselect-items-wrapper");

        public readonly By AddButton = By.CssSelector("app-tc-article-details button");

        public readonly By ArticalDetailsTable = By.CssSelector("app-tc-article-details div div.ui-g.ui-g-12.ui-g-nopad table");

        public ArticleDetailsPanel(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickField(string fieldName)
        {
            var fields = _driver.FindElements(By.CssSelector("app-tc-article-details form div span"));
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

        public void SelectMultiSelectionDropdownItem(string multiSelectionDropdownItem)
        {
            var items = _driver.FindElements(By.CssSelector(".ui-multiselect-items-wrapper li"));
            foreach (var item in items)
            {
                if (item.GetAttribute("aria-label").Trim().Equals(multiSelectionDropdownItem.Trim()))
                {
                    item.Click();
                    return;
                }
            }
        }

        public void EnterFieldValue(string fieldName, string Value)
        {
            var fields = _driver.FindElements(By.CssSelector("app-tc-article-details form div span"));
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

        public void ClickAddButton()
        {
            _driver.FindElement(AddButton).Click();
        }
    }
}