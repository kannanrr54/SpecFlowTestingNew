using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SpecflowTest.Pages
{
    internal class Login
    {
        IWebDriver driver;

        public Login(IWebDriver driver){
        this.driver = driver;
        }
        private By fName => By.Id("regFirstName");
        private By lName => By.Id("regLastName");
        private By dob => By.Id("regDob");
        private By submit => By.Id("regSubmit");

        public void enterFName(string name)
        {
            driver.FindElement(fName).SendKeys(name);
        }
        public void enterLName(string name)
        {
            driver.FindElement(lName).SendKeys(name);
        }
        public void enterDOB(string dobd)
        {
            driver.FindElement(dob).SendKeys(dobd);
        }
        public void Submit() { 

            driver.FindElement(submit).Click();
        }
    }
}
