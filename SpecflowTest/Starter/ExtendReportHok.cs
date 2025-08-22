using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Model;
using OpenQA.Selenium;
using Reqnroll;

namespace SpecflowTest.Starter
{
    [Binding]
    public static class ExtendReportHok
    {
        public static ExtentReports extenrreport;
        public static ExtentTest fetureTest;
        public static ExtentTest scenarioTest;

        public static ExtentReports extendScenario;
        public static ExtentTest extendScenarioTest;

        static string Reportpath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "\\EReport\\ExtendReport.html";
        static string Screenhotpath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "\\Screenshot";
        
        [BeforeTestRun]
        static void initializeReport()
        {   var htmlreport = new ExtentSparkReporter(Reportpath);
            extenrreport = new ExtentReports();
            extenrreport.AttachReporter(htmlreport);
        }
        [AfterTestRun]
        static void flushReport()
        {
            extenrreport.Flush();
        }
        [BeforeFeature]
        static void beforeFeature(FeatureContext featureContext)
        {
            fetureTest=extenrreport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }
        [BeforeScenario]
        static void beforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext) { 
            scenarioTest=fetureTest.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

            string scenarioFile = $"{scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.html";
            string fullPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "\\EReport", "Scenarios", scenarioFile);
            Console.WriteLine(fullPath);
            extendScenario = new ExtentReports();
            var htmlExtentScenario = new ExtentSparkReporter(fullPath);
            extendScenario.AttachReporter(htmlExtentScenario);
            extendScenarioTest =extendScenario.CreateTest(featureContext.FeatureInfo.Title+"/"+scenarioContext.ScenarioInfo.Title);
        }
        [AfterScenario]
        static void afterScenario(ScenarioContext scenarioContext)
        {
            extendScenario.Flush();
        }
        [AfterStep]
        static void afterStep(ScenarioContext scenarioContext) {
            string stepType=ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            string stepInfo = ScenarioStepContext.Current.StepInfo.Text;

            bool testCaseFail;
            if (scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.CreateNode<Given>(stepInfo);
                        extendScenarioTest.CreateNode<Given>(stepInfo); break;
                    case "When":
                        scenarioTest.CreateNode<When>(stepInfo);
                        extendScenarioTest.CreateNode<Given>(stepInfo); break;
                    case "Then":
                        scenarioTest.CreateNode<Then>(stepInfo);
                        extendScenarioTest.CreateNode<Given>(stepInfo); break;
                    case "And":
                        scenarioTest.CreateNode<And>(stepInfo);
                        extendScenarioTest.CreateNode<Given>(stepInfo); break;
                }
            }
            else
            {
                string errorDetails = scenarioContext.TestError?.Message;

                if (string.IsNullOrWhiteSpace(errorDetails))
                {
                    errorDetails = "No message";
                }
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.CreateNode<Given>(stepInfo+"--"+errorDetails).Fail();
                        extendScenarioTest.CreateNode<Given>(stepInfo + "--" + errorDetails).Fail(); break;
                    case "When":
                        scenarioTest.CreateNode<When>(stepInfo + "--" + errorDetails).Fail();
                        extendScenarioTest.CreateNode<Given>(stepInfo + "--" + errorDetails).Fail(); break;
                    case "Then":
                        scenarioTest.CreateNode<Then>(stepInfo + "--" + errorDetails).Fail();
                        extendScenarioTest.CreateNode<Given>(stepInfo + "--" + errorDetails).Fail(); break;
                    case "And":
                        scenarioTest.CreateNode<And>(stepInfo + "--" + errorDetails).Fail();
                        extendScenarioTest.CreateNode<Given>(stepInfo + "--" + errorDetails).Fail(); break;
                }
            }
        }

        public static void LogStep(string stepType, string stepInfo, Status status,
                                   ScenarioContext scenarioContext,
                                   bool takeScreenshot = false, string customMessage = "")
        {
            ExtentTest node = null;

            switch (stepType)
            {
                case "Given": node = scenarioTest.CreateNode<Given>(stepInfo); break;
                case "When": node = scenarioTest.CreateNode<When>(stepInfo); break;
                case "Then": node = scenarioTest.CreateNode<Then>(stepInfo); break;
                case "And": node = scenarioTest.CreateNode<And>(stepInfo); break;
                default: node = scenarioTest.CreateNode(stepInfo); break;
            }

            if (status == Status.Fail)
                node.Fail(customMessage == "" ? stepInfo : customMessage);
            else if (status == Status.Pass)
                node.Pass(customMessage == "" ? stepInfo : customMessage);
            else
                node.Info(customMessage == "" ? stepInfo : customMessage);

            // Screenshot logic
            if (takeScreenshot)
            {
                try
                {
                    var driver = scenarioContext.ContainsKey("driver")
                        ? (IWebDriver)scenarioContext["driver"]
                        : null;

                    if (driver != null)
                    {
                        string screenshotPath = CaptureScreenshot(driver);
                        node.AddScreenCaptureFromPath(screenshotPath);
                    }
                }
                catch (Exception ex)
                {
                    node.Warning("Screenshot capture failed: " + ex.Message);
                }
            }
        }

        private static string CaptureScreenshot(IWebDriver driver)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
            Directory.CreateDirectory(Screenhotpath);

            string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmssfff}.png";
            string filePath = Path.Combine(Screenhotpath, fileName);

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath);

            return filePath;
        }
    }
}
