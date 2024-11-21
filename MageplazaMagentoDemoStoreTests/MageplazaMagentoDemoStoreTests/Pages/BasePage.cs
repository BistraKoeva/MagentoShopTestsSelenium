using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        protected static readonly string BaseUrl = "https://magento-187465-0.cloudclusters.net";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public static string WelcomeMessageLoggedInXpath = "//div[@class='panel header']//span[@class='logged-in']";
        public IWebElement WelcomeMessageLoggedIn => driver.FindElement(By.XPath(WelcomeMessageLoggedInXpath));

        public static string WelcomeMessageNotLoggedInXpath = "//div[@class='panel header']//span[@class='not-logged-in']";
        public IWebElement WelcomeMessageNotLoggedIn => driver.FindElement(By.XPath(WelcomeMessageNotLoggedInXpath));

        public static string SignInLinkXpath = "//div[@class='panel header']//a[text()='Sign In']";
        public IWebElement SignInLink => driver.FindElement(By.XPath(SignInLinkXpath));

        public static string CreateAnAccountLinkXpath = "//div[@class='panel header']//a[text()='Create an Account']";
        public IWebElement CreateAnAccountLink => driver.FindElement(By.XPath(CreateAnAccountLinkXpath));



        public void NavigateToPage(string url)
        {
            driver.Navigate().GoToUrl(url);
        }        
    }
}
