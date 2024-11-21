using OpenQA.Selenium;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            
        }

        public static string Url => BaseUrl + "/";

    }
}
