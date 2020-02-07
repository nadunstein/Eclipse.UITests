using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Feature = AventStack.ExtentReports.Gherkin.Model.Feature;

namespace Browser.Tests.Hooks
{
    [Binding]
    public sealed class ExtentReportHooks
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;

        private string _stepType;
        private string _stepName;

        private static readonly string ExtentReportPath = Directory
            .GetParent(Directory
                .GetParent(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .ToString()).ToString()).ToString();

        public ExtentReportHooks(ScenarioContext scenarioContext, FeatureContext featureContext, IWebDriver driver)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driver = driver;
        }

        [BeforeTestRun(Order = 2)]
        public static void InitializeReport()
        {
            var htmlReporter =
                new ExtentHtmlReporter(Path.Combine(ExtentReportPath, "dashboard.html"));
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Eclipse UI tests";
            htmlReporter.Config.ReportName = "Eclipse UI tests";

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario()
        {
            if (_featureName is null)
            {
                _featureName = _extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            }

            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            _stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            _stepName = ScenarioStepContext.Current.StepInfo.Text;
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            if (_scenarioContext.TestError == null)
            {
                switch (_stepType)
                {
                    case "Given":
                        _scenario.CreateNode<Given>("<b>" + _stepType + "</b>" + " " + _stepName);
                        break;
                    case "When":
                        _scenario.CreateNode<When>("<b>" + _stepType + "</b>" + " " + _stepName);
                        break;
                    case "Then":
                        _scenario.CreateNode<Then>("<b>" + _stepType + "</b>" + " " + _stepName);
                        break;
                }
            }

            else if (_scenarioContext.TestError != null)
            {
                var ss = ((ITakesScreenshot)_driver).GetScreenshot();
                var screenshot = ss.AsBase64EncodedString;

                switch (_stepType)
                {
                    case "Given":
                        _scenario.CreateNode<Given>("<b>" + _stepType + "</b>" + " " + _stepName)
                            .Fail(_scenarioContext.TestError.Message,
                                MediaEntityBuilder
                                    .CreateScreenCaptureFromBase64String(screenshot, "Fail Image")
                                    .Build());
                        break;
                    case "When":
                        _scenario.CreateNode<When>("<b>" + _stepType + "</b>" + " " + _stepName)
                            .Fail(_scenarioContext.TestError.Message,
                                MediaEntityBuilder
                                    .CreateScreenCaptureFromBase64String(screenshot, "fail Image")
                                    .Build());
                        break;
                    case "Then":
                        _scenario.CreateNode<Then>("<b>" + _stepType + "</b>" + " " + _stepName)
                            .Fail(_scenarioContext.TestError.Message,
                                MediaEntityBuilder
                                    .CreateScreenCaptureFromBase64String(screenshot, "Fail Image")
                                    .Build());
                        break;
                }
            }
        }

        [AfterScenario(Order = 1)]
        public void AfterScenario()
        {
            var pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus",
                BindingFlags.Instance | BindingFlags.Public);
            if (pInfo == null)
            {
                return;
            }

            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object testResult = getter.Invoke(_scenarioContext, null);

            if (testResult.ToString() != "UndefinedStep")
            {
                return;
            }

            switch (_stepType)
            {
                case "Given":
                    _scenario.CreateNode<Given>("<b>" + _stepType + "</b>" + " " + _stepName)
                        .Skip("Step Definition Pending");
                    break;
                case "When":
                    _scenario.CreateNode<When>("<b>" + _stepType + "</b>" + " " + _stepName)
                        .Skip("Step Definition Pending");
                    break;
                case "Then":
                    _scenario.CreateNode<Then>("<b>" + _stepType + "</b>" + " " + _stepName)
                        .Skip("Step Definition Pending");
                    break;
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            _featureName = null;
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extent.Flush();
        }
    }
}