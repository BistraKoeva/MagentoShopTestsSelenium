using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class ProductsPage(IWebDriver driver) : BasePage(driver)
    {
        public static string MainSideBarXpath = "//div[@class='sidebar sidebar-main']";
        public IWebElement MainSideBar => driver.FindElement(By.XPath(MainSideBarXpath));

        public static string AllProductDetailsItemsXpath = "//div[@class='product details product-item-details']";
        public ReadOnlyCollection<IWebElement> AllProductDetailsItems => driver.FindElements(By.XPath(AllProductDetailsItemsXpath));

        public static string SuccessMessageXpath = "//div[@data-ui-id='message-success']";
        public IWebElement SuccessMessage => driver.FindElement(By.XPath(SuccessMessageXpath));

        public ProductObject GetProductByIndex(int productIndex)
        {
            ProductObject product = new(driver, AllProductDetailsItems[productIndex]);
            return product;
        }
    }
}
