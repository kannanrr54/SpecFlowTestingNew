using System;
using System.Xml.Linq;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.Audits;
using Reqnroll;
using SpecflowTest.Pages;
using SpecflowTest.Starter;

namespace SpecflowTest.StepDefinitions
{
    [Binding]
    public class RegistrationOfUsersStepDefinitions 
    {
        public ScenarioContext scenarioContext;
        public XDocument xmlD;
        public IWebDriver driver;
        public RegistrationOfUsersStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            if(scenarioContext["XmlTestData"]!=null)
            this.xmlD = (XDocument)scenarioContext["XmlTestData"];
            this.driver = (IWebDriver)scenarioContext["driver"];
        }
        [When("User enters registration data")]
        public void WhenUserEntersRegistrationData()
        {
            try
            {
                Login login = new Login(driver);
                login.enterFName(xmlD.Root.Element("FirstName").Value);
                login.enterLName(xmlD.Root.Element("LastName").Value);
                login.enterDOB(xmlD.Root.Element("DOB").Value);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            
        }
        [When("user enters {string} and {string} as fname and lname")]
        public void WhenUserEntersAndAsFnameAndLname(string kannan, string rr)
        {
            Console.WriteLine(kannan+"-----"+rr);
        }

        [Then("Error comes")]
        public void ThenErrorComes()
        {
            
        }


        [Then("click on submit button")]
        public void ThenClickOnSubmitButton()
        {
            Login login = new Login(driver);
            login.Submit();
        }

        [Then("USer registration successfull")]
        public void ThenUSerRegistrationSuccessfull()
        {
            TestContext.WriteLine("--------success--------");
        }

        [Then("enters in country")]
        public void ThenEntersInCountry(DataTable dataTable)
        {
            ExtendReportHok.LogStep(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString(), ScenarioStepContext.Current.StepInfo.Text.ToString(), Status.Pass, scenarioContext, true, "Entered test data successfully");
            foreach (var i in dataTable.Rows)
            {
                Console.WriteLine(i["city"]);
            }
            Assert.Fail("bla bla");
            //try { Assert.Fail(); }
            //catch(Exception e)
            //{
            //    scenarioContext["TestError"] = e;
            //    throw;
            //}

        }

    }
}
