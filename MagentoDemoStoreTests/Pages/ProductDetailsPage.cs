using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class ProductDetailsPage(IWebDriver driver) : BasePage(driver)
    {
        public static string AllSizesXpath = "//div[contains(@class, 'swatch-option text')]";
        public ReadOnlyCollection<IWebElement> AllSizesElements => driver.FindElements(By.XPath(AllSizesXpath));
        public System.Collections.Generic.List<string> AllSizesText = new List<string>();

        public static string AllColorsXpath = "//div[contains(@class, 'swatch-option color')]";
        public ReadOnlyCollection<IWebElement> AllColorsElements => driver.FindElements(By.XPath(AllColorsXpath));
        public System.Collections.Generic.List<string> AllColorsText = new List<string>();

        public static string UpdateCartButtonXpath = "//button[@title='Update Cart']";
        public IWebElement UpdateCartButton => driver.FindElement(By.XPath(UpdateCartButtonXpath));

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

        public void EditProductSize(string size)
        {
            ProcessColorsAndSizesTexts();
            int indexSize = AllSizesText.IndexOf(size);
            if (indexSize >= 0)
            {
                AllSizesElements[indexSize].Click();
            }
            
            UpdateCartButton.Click();
        }
    }
}
