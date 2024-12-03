using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class MainCategoryPage(IWebDriver driver) : BasePage(driver)
    {
        public static string MainSideBarXpath = "//div[@class='sidebar sidebar-main']";
        public IWebElement MainSideBar => driver.FindElement(By.XPath(MainSideBarXpath));

        public static string AllLinksInMainSideBarXpath = "//div[@class='sidebar sidebar-main']//li/a";
        public ReadOnlyCollection<IWebElement> AllLinksInMainSideBar => driver.FindElements(By.XPath(AllLinksInMainSideBarXpath));

        public System.Collections.Generic.List<string> AllCategoriesInMainSideBarTexts = new List<string>();

        private void ProcessCategoriesFromMainSideBarTexts()
        {
            foreach (var link in AllLinksInMainSideBar)
            {
                AllCategoriesInMainSideBarTexts.Add(link.Text);
            }
        }

        public ProductsPage ChooseCategoryFromMainSideBar(string category)
        {
            ProcessCategoriesFromMainSideBarTexts();

            int index = AllCategoriesInMainSideBarTexts.IndexOf(category);
            if (index >= 0)
            {
                AllLinksInMainSideBar[index].Click();
            }

            var productsPage = new ProductsPage(driver);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ProductsPage.MainSideBarXpath)));

            return productsPage;
        }
    }
}
