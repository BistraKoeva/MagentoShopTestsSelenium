using MagentoDemoStoreTestsPOM.Pages;

namespace MagentoDemoStoreTestsPOM.Tests
{
    public class SignInTests : BaseTest
    {
        [Test]
        public void SignInWithValidCredentials()
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);

            MyAccountPage myAccountPage = signInPage.SignInWithValidCredentials("test@test.bg", "Bistra123");

            Assert.IsNotNull(myAccountPage.Title, "The page title 'My Account' should be displayed.");
            Assert.That(driver.Url, Is.EqualTo(MyAccountPage.Url), "The user should be redirected to the My account page.");
           // Assert.That(myAccountPage.WelcomeMessageLoggedIn.Text, Is.EqualTo("Welcome, Bistra Koeva!"), "The user should be signed-in and the welcome message should be present.");
        }

        //Some of the tests below don't pass in some cases because of Chrome HTML5 interactive form validation messages

        [Test]
        [TestCase("bistra.bg", "Bistra12")]
        [TestCase("bistra@.bg", "Bistra12")]
        [TestCase("@bg.com", "Bistra12")]
        [TestCase("bistra@abv", "Bistra12")]
        [TestCase("bistra@@abv.bg", "Bistra12")]
        [TestCase("bistra!@abv.bg", "Bistra12")]
        [TestCase("bistra@abv..bg", "Bistra12")]
        [TestCase("bistra @abv.bg", "Bistra12")]
        [TestCase("bistra@abv.c", "Bistra12")]
        [TestCase("bistra@abv.com.", "Bistra12")]
        [TestCase(".bistra@abv.com", "Bistra12")]
        public void TryToSignInWithInvalidEmail(string invalidEmail, string password)
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);
            signInPage.SignInWithInvalidEmail(invalidEmail, password);

            Assert.That(driver.Url, Is.EqualTo(SignInPage.Url), "The user should stay on the Sign-in Page.");
            Assert.That(signInPage.EmailErrorMessage.Text, Is.EqualTo("Please enter a valid email address (Ex: johndoe@domain.com)."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToSignInWithEmptyEmail()
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);
            signInPage.SignInWithEmptyInput("", "Bistra123");

            Assert.That(driver.Url, Is.EqualTo(SignInPage.Url), "The user should stay on the Sign-in Page.");
            Assert.That(signInPage.EmailErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToSignInWithEmptyPassword()
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);
            signInPage.SignInWithEmptyInput("test@test.bg", "");

            Assert.That(driver.Url, Is.EqualTo(SignInPage.Url), "The user should stay on the Sign-in Page.");
            Assert.That(signInPage.PassErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToSignInWithIncorrectPassword()
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);
            signInPage.SignInWithWrongCredentials("test@test.bg", "Bistra1234");

            Assert.That(driver.Url, Is.EqualTo(SignInPage.Url), "The user should stay on the Sign-in Page.");
            Assert.That(signInPage.WrongCredentialsErrorMessage.Text, Is.EqualTo("The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later."), "The correct error message should be displayed.");
        }

        [Test]
        public void TryToSignInWithIncorrectEmail()
        {
            SignInPage signInPage = new SignInPage(driver);
            signInPage.NavigateToPage(SignInPage.Url);
            signInPage.SignInWithWrongCredentials("testW@test.com", "Bistra123");

            Assert.That(driver.Url, Is.EqualTo(SignInPage.Url), "The user should stay on the Sign-in Page.");
            Assert.That(signInPage.WrongCredentialsErrorMessage.Text, Is.EqualTo("The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later."), "The correct error message should be displayed.");
        }
    }
}