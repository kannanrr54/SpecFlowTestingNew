using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace SpecflowTest.Starter
{
    [Binding]
    public class Starter
    {
        private static IWebDriver driver;
        private  ScenarioContext scenarioContext;
        private  FeatureContext featureContext;
        Starter(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        [BeforeScenario]
        void setup()
        {
            XDocument XmlData=null;
            string scenarioTag;
            driver = new ChromeDriver();
            driver.Url="file:///C:/Desktop/bdd.html";
            driver.Manage().Window.Maximize();
            try
            {
                scenarioTag = scenarioContext.ScenarioInfo.Tags.FirstOrDefault(t => t.StartsWith("xml")).Split("-")[1];
            }
            catch (Exception ex) {
                scenarioTag = null;
            }
            if (scenarioTag != null)
            {
                string dataDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "\\TestData\\" + scenarioTag;
                if (Directory.Exists(dataDir))
                {
                    XmlData = XDocument.Load(dataDir);
                }
            }
            scenarioContext["XmlTestData"] = XmlData;
            scenarioContext["driver"] = driver;
        }
    }
}
