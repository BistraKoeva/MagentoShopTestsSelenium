using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class CartMiniViewPage(IWebDriver driver) : BasePage(driver)
    {
        public static string SeeDetailsLinkXpath = "//span[text()='See Details']";
        public ReadOnlyCollection<IWebElement> SeeDetailsLinks => driver.FindElements(By.XPath(SeeDetailsLinkXpath));

        public static string CartSubtotalXpath = "//div[@class='amount price-container']//span[@class='price']";

        public IWebElement CartSubtotalElement => driver.FindElement(By.XPath(CartSubtotalXpath));

        public static string ItemsTotalCountXpath = "//div[@class='items-total']//span[@class='count']";
        public IWebElement ItemsTotalCountElement => driver.FindElement(By.XPath(ItemsTotalCountXpath));

        public static string ProductNamesXpath = "//strong[@class='product-item-name']";
        public ReadOnlyCollection<IWebElement> ProductNamesElements => driver.FindElements(By.XPath(ProductNamesXpath));

        // tova ne e dobre
        //public static string ProductsSizesAndColorsXpath = "//span[@data-bind='text: option.value']";
        public static string ProductsSizesAndColorsXpath = "//dl[@class='product options list']//dd";
        public ReadOnlyCollection<IWebElement> ProductsSizesAndColorsElements => driver.FindElements(By.XPath(ProductsSizesAndColorsXpath));

        public static string ProductsPricesXpath = "//span[@class='minicart-price']//span[@class='price']";
        public ReadOnlyCollection<IWebElement> ProductsPricesElements => driver.FindElements(By.XPath(ProductsPricesXpath));

        public static string EditLinksXpath = "//a[@class='action edit']";
        public ReadOnlyCollection<IWebElement> EditLinks => driver.FindElements(By.XPath(EditLinksXpath));

        public static string DeleteLinksXpath = "//a[@class='action delete']";
        public ReadOnlyCollection<IWebElement> DeleteLinks => driver.FindElements(By.XPath(DeleteLinksXpath));

        public static string ConfirmDeletionButtonXpath = "//button[@class='action-primary action-accept']";
        public IWebElement ConfirmDeletionButton => driver.FindElement(By.XPath(ConfirmDeletionButtonXpath));

        public static string NoItemsInCartMessageXpath = "//div[@class='block-content']//strong[@class='subtitle empty']";
        public IWebElement NoItemsInCartMessage => driver.FindElement(By.XPath(NoItemsInCartMessageXpath));

        public static string ProductsQuantityInputsXpath = "//input[@class='item-qty cart-item-qty']";
        public ReadOnlyCollection<IWebElement> ProductsQuantityInputs => driver.FindElements(By.XPath(ProductsQuantityInputsXpath));

        public static string UpdateQuantityButtonsXpath = "//button[@class='update-cart-item']";
        public ReadOnlyCollection<IWebElement> UpdateQuantityButtons => driver.FindElements(By.XPath(UpdateQuantityButtonsXpath));

        public void ExpandCollapseSeeDetails(int index)
        {
            wait.Until(d => SeeDetailsLinks[index].Enabled);
            SeeDetailsLinks[index].Click(); 
        }

        public void UpdateQuantityOfProduct(int index)
        {
            string initialQuantity = ItemsTotalCountElement.Text;
            wait.Until(d => UpdateQuantityButtons[index].Enabled);
            UpdateQuantityButtons[index].Click();
            wait.Until(d =>  initialQuantity != ItemsTotalCountElement.Text);
        }

        public void DeleteProduct(int index)
        {
            string initialQuantity = ItemsTotalCountElement.Text;
            wait.Until(ExpectedConditions.ElementToBeClickable(DeleteLinks[index]));
            bool isLastProductInCart = DeleteLinks.Count == 1;
            DeleteLinks[index].Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(ConfirmDeletionButton));
            ConfirmDeletionButton.Click();
            wait.Until(d => initialQuantity != ItemsTotalCountElement.Text);
            if (isLastProductInCart)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(NoItemsInCartMessageXpath)));
            }
        }

        public ProductDetailsPage ClickEditProductLink (int index)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(EditLinks[index]));
            EditLinks[index].Click();

            ProductDetailsPage productDetailsPage = new(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ProductDetailsPage.UpdateCartButtonXpath)));
            return productDetailsPage;
        }
    }
}
