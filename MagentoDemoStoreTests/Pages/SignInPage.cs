using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class SignInPage(IWebDriver driver) : BasePage(driver)
    {
        public static string Url => BaseUrl + "/customer/account/login/";

        // Fields in the Sign-in form
        public static string EmailFieldXpath = "//input[@id='email']";
        public IWebElement EmailField => driver.FindElement(By.XPath(EmailFieldXpath));

        public static string PasswordFieldXpath = "//input[@id='password' and @name='login[password]']";
        public IWebElement PasswordField => driver.FindElement(By.XPath(PasswordFieldXpath));

        public static string ShowPasswordCheckBoxXpath = "//input[@id='show-password']";
        public IWebElement ShowPasswordCheckBox => driver.FindElement(By.XPath(ShowPasswordCheckBoxXpath));

        public static string SignInButtonXpath = "//button[@id='send2' and @class='action login primary']";
        public IWebElement SignInButton => driver.FindElement(By.XPath(SignInButtonXpath));

        // Error/validation messages
        public static string EmailErrorMessageXpath = "//div[@id='email-error']";
        public IWebElement EmailErrorMessage => driver.FindElement(By.XPath(EmailErrorMessageXpath));

        public static string PassErrorMessageXpath = "//div[@id='password-error']";
        public IWebElement PassErrorMessage => driver.FindElement(By.XPath(PassErrorMessageXpath));

        public static string WrongCredentialsErrorMessageXpath = "//div[@class='message-error error message']//div";
        public IWebElement WrongCredentialsErrorMessage => driver.FindElement(By.XPath(WrongCredentialsErrorMessageXpath));

        private void FillAndSubmitSignInForm(string email, string password)
        {
            

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailFieldXpath)));
            EmailField.Clear();
            EmailField.SendKeys(email);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordFieldXpath)));
            PasswordField.Clear();
            PasswordField.SendKeys(password);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ShowPasswordCheckBoxXpath)));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(BasePage.WelcomeMessageNotLoggedInXpath)));
            
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(SignInButtonXpath)));
            SignInButton.Click();
        }

        public MyAccountPage SignInWithValidCredentials(string email, string password)
        {
            FillAndSubmitSignInForm(email, password);

            var myAccountPage = new MyAccountPage(driver);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MyAccountPage.TitleXpath)));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(BasePage.WelcomeMessageLoggedInXpath)));

            return myAccountPage;
        }

        public void SignInWithEmptyInput(string email, string password)
        {
            FillAndSubmitSignInForm(email, password);

            if (email == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailErrorMessageXpath)));
            }

            if (password == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PassErrorMessageXpath)));
            }
        }

        public void SignInWithInvalidEmail(string email, string password)
        {
            FillAndSubmitSignInForm(email, password);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailErrorMessageXpath)));

        }

        public void SignInWithWrongCredentials(string email, string password)
        {
            FillAndSubmitSignInForm(email, password);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(WrongCredentialsErrorMessageXpath)));

        }
    }
}
