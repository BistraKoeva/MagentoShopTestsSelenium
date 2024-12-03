using OpenQA.Selenium;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class HomePage(IWebDriver driver) : BasePage(driver)
    {
        public static string Url => BaseUrl + "/";

    }
}
