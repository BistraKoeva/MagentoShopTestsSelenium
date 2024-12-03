using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class BasePage(IWebDriver driver)
    {
        protected IWebDriver driver = driver;
        protected WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        protected Actions actions = new Actions(driver);

        protected static readonly string BaseUrl = "https://magento-187465-0.cloudclusters.net";


        public static string WelcomeMessageLoggedInXpath = "//div[@class='panel header']//span[@class='logged-in']";
        public IWebElement WelcomeMessageLoggedIn => driver.FindElement(By.XPath(WelcomeMessageLoggedInXpath));

        public static string WelcomeMessageNotLoggedInXpath = "//div[@class='panel header']//span[@class='not-logged-in']";
        public IWebElement WelcomeMessageNotLoggedIn => driver.FindElement(By.XPath(WelcomeMessageNotLoggedInXpath));

        public static string SignInLinkXpath = "//div[@class='panel header']//a[text()='Sign In']";
        public IWebElement SignInLink => driver.FindElement(By.XPath(SignInLinkXpath));

        public static string CreateAnAccountLinkXpath = "//div[@class='panel header']//a[text()='Create an Account']";
        public IWebElement CreateAnAccountLink => driver.FindElement(By.XPath(CreateAnAccountLinkXpath));

        public static string SearchInputXpath = "//input[@id='search']";
        public IWebElement SearchInput => driver.FindElement(By.XPath(SearchInputXpath));

        public static string CartLinkXpath = "//a[@class='action showcart']";
        public IWebElement CartLink => driver.FindElement(By.XPath(CartLinkXpath));

        public static string CartCounterNumberXpath = "//span[@class='counter-number']";
        public IWebElement CartCounterNumber => driver.FindElement(By.XPath(CartCounterNumberXpath));

        public static string WomenNavbarLinkXpath = "//a[@id='ui-id-3']";
        public IWebElement WomenNavbarLink => driver.FindElement(By.XPath(WomenNavbarLinkXpath));

        public void NavigateToPage(string url)
        {
            driver.Navigate().GoToUrl(url);
        }     
        
        public MainCategoryPage ChooseMainCategoryFromNavbar(string mainCategoryNavbarLinkXpath)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(mainCategoryNavbarLinkXpath)));

            IWebElement mainCategoryNavbarLink = driver.FindElement(By.XPath(mainCategoryNavbarLinkXpath));

            mainCategoryNavbarLink.Click();

            var mainCategoryPage = new MainCategoryPage(driver);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MainCategoryPage.MainSideBarXpath)));

            return mainCategoryPage;
        }

        public CartMiniViewPage OpenCartMiniView()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(CartLinkXpath)));
            CartLink.Click();

            var cartMiniViewPage = new CartMiniViewPage(driver);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(CartMiniViewPage.SeeDetailsLinkXpath)));

            return cartMiniViewPage;
        }
    }
}
