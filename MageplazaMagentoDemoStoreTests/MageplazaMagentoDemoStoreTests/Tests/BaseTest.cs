using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MagentoDemoStoreTestsPOM.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();   
        }
    }
}
