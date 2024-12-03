using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace MagentoDemoStoreTestsPOM.Pages
{
    public class CreateAccountPage(IWebDriver driver) : BasePage(driver)
    {

        // Fields in the Create an account form
        public static string Url => BaseUrl + "/customer/account/create/";

        public static string FirstNameFieldXpath = "//input[@id='firstname']";
        public IWebElement FirstNameField => driver.FindElement(By.XPath(FirstNameFieldXpath));

        public static string LastNameFieldXpath = "//input[@id='lastname']";
        public IWebElement LastNameField => driver.FindElement(By.XPath(LastNameFieldXpath));

        public static string EmailFieldXpath = "//input[@id='email_address']";
        public IWebElement EmailField => driver.FindElement(By.XPath(EmailFieldXpath));

        public static string PasswordFieldXpath = "//input[@id='password']";
        public IWebElement PasswordField => driver.FindElement(By.XPath(PasswordFieldXpath));

        public static string PasswordConfirmationFieldXpath = "//input[@id='password-confirmation']";
        public IWebElement PasswordConfirmationField => driver.FindElement(By.XPath(PasswordConfirmationFieldXpath));

        public static string ShowPasswordCheckBoxXpath = "//input[@id='show-password']";
        public IWebElement ShowPasswordCheckBox => driver.FindElement(By.XPath(ShowPasswordCheckBoxXpath));

        public static string CreateAccountButtonXpath = "//button[@id='send2' and @class='action submit primary']";
        public IWebElement CreateAccountButton => driver.FindElement(By.XPath(CreateAccountButtonXpath));

        // Error/validation messages
        public static string FirstNameErrorMessageXpath = "//div[@id='firstname-error']";
        public IWebElement FirstNameErrorMessage => driver.FindElement(By.XPath(FirstNameErrorMessageXpath));

        public static string LastNameErrorMessageXpath = "//div[@id='lastname-error']";
        public IWebElement LastNameErrorMessage => driver.FindElement(By.XPath(LastNameErrorMessageXpath));

        public static string EmailErrorMessageXpath = "//div[@id='email_address-error']";
        public IWebElement EmailErrorMessage => driver.FindElement(By.XPath(EmailErrorMessageXpath));

        public static string PasswordStrengthLabelXpath = "//span[@id='password-strength-meter-label']";
        public IWebElement PasswordStrengthLabel => driver.FindElement(By.XPath(PasswordStrengthLabelXpath));

        public static string PasswordErrorMessageXpath = "//div[@id='password-error']";
        public IWebElement PasswordErrorMessage => driver.FindElement(By.XPath(PasswordErrorMessageXpath));

        public static string PasswordConfirmationErrorMessageXpath = "//div[@id='password-confirmation-error']";
        public IWebElement PasswordConfirmationErrorMessage => driver.FindElement(By.XPath(PasswordConfirmationErrorMessageXpath));


        private void FillAndSubmitCreateAccountForm(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(FirstNameFieldXpath)));
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(LastNameFieldXpath)));
            LastNameField.Clear();
            LastNameField.SendKeys(lastName);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailFieldXpath)));
            EmailField.Clear();
            EmailField.SendKeys(email);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordFieldXpath)));
            PasswordField.Clear();
            PasswordField.SendKeys(password);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordConfirmationFieldXpath)));
            PasswordConfirmationField.Clear();
            PasswordConfirmationField.SendKeys(confirmPassword);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ShowPasswordCheckBoxXpath)));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(BasePage.WelcomeMessageNotLoggedInXpath)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(CreateAccountButtonXpath)));
            actions.ScrollToElement(CreateAccountButton).Perform();
            CreateAccountButton.Click();
        }

        public MyAccountPage CreateAccountWithValidData(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FillAndSubmitCreateAccountForm(firstName, lastName, email, password, confirmPassword);

            var myAccountPage = new MyAccountPage(driver);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MyAccountPage.TitleXpath)));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MyAccountPage.NewAccountSuccessMessageXpath)));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(BasePage.WelcomeMessageLoggedInXpath)));

            return myAccountPage;
        }

        public void CreateAccountWithEmptyInput(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FillAndSubmitCreateAccountForm(firstName, lastName, email, password, confirmPassword);

            if (firstName == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(FirstNameErrorMessageXpath)));
            }

            if (lastName == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(LastNameErrorMessageXpath)));
            }

            if (email == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailErrorMessageXpath)));
            }

            if (password == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordErrorMessageXpath)));
            }

            if (confirmPassword == "")
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordConfirmationErrorMessageXpath)));
            }
        }

        public void CreateAccountWithInvalidEmail(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FillAndSubmitCreateAccountForm(firstName, lastName, email, password, confirmPassword);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(EmailErrorMessageXpath)));
        }

        public void CreateAccountWithInvalidPassword(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FillAndSubmitCreateAccountForm(firstName, lastName, email, password, confirmPassword);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordErrorMessageXpath)));
        }

        public void CreateAccountWithNotMatchingConfirmPassword(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FillAndSubmitCreateAccountForm(firstName, lastName, email, password, confirmPassword);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PasswordConfirmationErrorMessageXpath)));    
        }

        public string GenerateUniqueEmail()
        {
            string timeNow = (DateTimeOffset.Now.ToUnixTimeSeconds()).ToString();
            string email = $"test{timeNow}@abv.bg";
            return email;
        }
    }
}
