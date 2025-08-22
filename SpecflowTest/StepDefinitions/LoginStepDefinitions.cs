using System;
using NUnit.Framework;
using Reqnroll;

namespace SpecflowTest.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {

        [Given("User navigated to the login page")]
        public void GivenUserNavigatedToTheLoginPage()
        {
            Console.WriteLine("User navigated to the login page");
        }

        [When("User enters in correct username and password")]
        public void WhenUserEntersInCorrectUsernameAndPassword()
        {
            Console.WriteLine("User enters in correct username and password");
        }

        [When("User enters in incorrect username and password")]
        public void WhenUserEntersInIncorrectUsernameAndPassword()
        {
            Console.WriteLine("User enters in incorrect username and password");
        }

        [Then("User navigates to home page")]
        public void ThenUserNavigatesToHomePage()
        {
            Console.WriteLine("User navigates to home page");
        }    
    }
}
