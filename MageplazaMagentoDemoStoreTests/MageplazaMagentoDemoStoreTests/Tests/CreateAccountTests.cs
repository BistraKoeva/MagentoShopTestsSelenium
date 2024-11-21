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
            Assert.That(myAccountPage.WelcomeMessageLoggedIn.Text, Does.StartWith("Welcome"), "The user should be signed-in and the welcome message should be present.");
        }

        [Test]
        public void CreateAccountWithEmptyFirstName()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.NavigateToPage(CreateAccountPage.Url);

            string validEmail = createAccountPage.GenerateUniqueEmail();
            createAccountPage.CreateAccountWithEmptyInput("", "Koeva", validEmail, "valid12!", "valid12!");

            Assert.That(driver.Url, Is.EqualTo(CreateAccountPage.Url), "The user should stay on the Create Account Page.");
            Assert.That(createAccountPage.FirstNameErrorMessage.Text, Is.EqualTo("This is a required field."), "The correct error message should be displayed.");
        }
    }
}
