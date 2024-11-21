﻿using OpenQA.Selenium;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class MyAccountPage : BasePage
    {
        public MyAccountPage(IWebDriver driver) : base(driver)
        {
            
        }

        public static string Url => BaseUrl + "/customer/account/";


        public static string TitleXpath = "//span[text()='My Account']";
        public IWebElement Title => driver.FindElement(By.XPath(TitleXpath));

        public static string NewAccountSuccessMessageXpath = "//div[@class='message-success success message']//div";
        public IWebElement NewAccountSuccessMessage => driver.FindElement(By.XPath(NewAccountSuccessMessageXpath));
    }
}