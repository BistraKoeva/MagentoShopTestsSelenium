using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class ProductObject(IWebDriver driver, IWebElement productDetails)
    {
        protected IWebDriver driver = driver;
        protected WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        protected Actions actions = new Actions(driver);
        protected IWebElement ProductDetails = productDetails;

        public string Name = "";
        public string Price = "";
        public string Color = "";
        public string Size = "";

        public static string AllSizesXpath = ".//div[@class='swatch-option text']";
        public ReadOnlyCollection<IWebElement> AllSizesElements => ProductDetails.FindElements(By.XPath(AllSizesXpath));

        public System.Collections.Generic.List<string> AllSizesText = new List<string>();

        public static string AllColorsXpath = ".//div[@class='swatch-option color']";
        public ReadOnlyCollection<IWebElement> AllColorsElements => ProductDetails.FindElements(By.XPath(AllColorsXpath));

        public System.Collections.Generic.List<string> AllColorsText = new List<string>();

        public static string PriceXpath = ".//div[@class='price-box price-final_price']//span[@class='price']";

        public static string ProductItemLinkXpath = ".//a[@class='product-item-link']";
        public IWebElement ProductItemLink => ProductDetails.FindElement(By.XPath(ProductItemLinkXpath));

        public static string AddToCartButtonXpath = ".//button[@title='Add to Cart']";
        public IWebElement AddToCartButton => ProductDetails.FindElement(By.XPath(AddToCartButtonXpath));

        
        private void ProcessColorsAndSizesTexts()
        {
            foreach (var sizeElement in AllSizesElements)
            {
                AllSizesText.Add(sizeElement.Text);
            }

            foreach (var colorElement in AllColorsElements)
            {
                AllColorsText.Add(colorElement.GetAttribute("data-option-label"));
            }
        }

        public void SelectSizeAndColorAndAddToCart(string size, string color)
        {
            ProcessColorsAndSizesTexts();
            int indexSize = AllSizesText.IndexOf(size);
            if (indexSize >= 0)
            {
                AllSizesElements[indexSize].Click();
                Size = size;
            }
            int indexColor = AllColorsText.IndexOf(color);
            if (indexColor >= 0)
            {
                AllColorsElements[indexColor].Click();
                Color = color;
            }
            actions.MoveToElement(ProductDetails).Perform();
            wait.Until(ExpectedConditions.ElementToBeClickable(AddToCartButton));
            AddToCartButton.Click();

            if (indexColor >= 0 && indexSize >= 0)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(ProductsPage.SuccessMessageXpath)));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(BasePage.CartCounterNumberXpath)));
            }

            Name = ProductItemLink.Text;
            Price = ProductDetails.FindElement(By.XPath(PriceXpath)).Text;
        }
    }
}
