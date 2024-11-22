using MagentoDemoStoreTestsPOM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoDemoStoreTestsPOM.Tests
{
    public class CreateAccountTests : BaseTest
    {
        [Test]
        public void CreateAccountWithValidCredentials()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            MyAccountPage myAccountPage = createAccountPage.CreateAccountWithValidData("Bistra", "Koeva", validEmail, "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(MyAccountPage.Url), "The user should be redirected to the My account page.");
            Assert.IsNotNull(myAccountPage.Title, "The page title 'My Account' should be displayed.");
            Assert.That(myAccountPage.NewAccountSuccessMessage.Text, Is.EqualTo("Thank you for registering with Main Website Store."));
            Assert.That(myAccountPage.WelcomeMessageLoggedIn.Text, Is.EqualTo("Welcome, Bistra Koeva!"), "The user should be signed-in and the welcome message should be present.");
        }

        //Some of the tests below don't pass in some cases because of Chrome HTML5 interactive form validation messages

        [Test]
        public void TryToCreateAccountWithEmptyFirstName()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("", "Koeva", validEmail, "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.FirstNameErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithEmptyLastName()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "", validEmail, "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.LastNameErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithEmptyEmail()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", "", "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.EmailErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithEmptyPassword()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", validEmail, "", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.PasswordErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithEmptyConfirmPassword()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", validEmail, "valid12!", "");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.PasswordConfirmationErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithNotMatchingConfirmPassword()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", validEmail, "valid12!", "!valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.PasswordConfirmationErrorMessage.Text, Is.EqualTo("Please enter the same value again."), "The correct error message should be displayed.");
        }

        [Test]
        [TestCase("bistra.bg")]
        [TestCase("bistra@.bg")]
        [TestCase("@bg.com")]
        [TestCase("bistra@abv")]
        [TestCase("bistra@@abv.bg")]
        [TestCase("bistra!@abv.bg")]
        [TestCase("bistra@abv..bg")]
        [TestCase("bistra @abv.bg")]
        [TestCase("bistra@abv.c")]
        [TestCase("bistra@abv.com.")]
        [TestCase(".bistra@abv.com")]

        public void TryToCreateAccountWithInvalidEmail(string invalidEmail)
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            createAccountPage.CreateAccountWithInvalidEmail("Bistra", "Koeva", invalidEmail, "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.EmailErrorMessage.Text, Is.EqualTo("Please enter a valid email address (Ex: johndoe@domain.com)."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToCreateAccountWithPasswordBelowTheMinimumSymbols() // Password must be at least 8 symbols.
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", validEmail, "Short!!", "Short!!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.PasswordErrorMessage.Text, Is.EqualTo("Minimum length of this field must be equal or greater than 8 symbols. Leading and trailing spaces will be ignored."), "The correct error message should be displayed.");
        }

        [Test]
        [TestCase("INvalidd")]
        [TestCase("invalid1")]
        [TestCase("invalid!")]
        [TestCase("INVALID!")]
        [TestCase("INVALID1")]
        [TestCase("1234!@$%")]
        public void TryToCreateAccountWithInvalidPasswordLessThan3ClassesOfCharacters(string invalidPassword)
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("Bistra", "Koeva", validEmail, invalidPassword, invalidPassword);

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.PasswordErrorMessage.Text, Is.EqualTo("Minimum of different classes of characters in password is 3. Classes of characters: Lower Case, Upper Case, Digits, Special Characters."), "The correct error message should be displayed.");
        }
    }
}
