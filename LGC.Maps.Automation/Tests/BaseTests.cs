using System;
using LGC.AutoFramework.Helper;
using LGC.Maps.Automation.Browser.PageObjects;
using LGC.Maps.Automation.Browser.Widgets;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LGC.Maps.Automation.Tests
{
    public class BaseTests
    {
        #region Properties Constants
        private const string EnvName = "Develop";
        public static IWebDriver Driver { get; private set; }
        public static string AssemblyPath { get; private set; }
        public static AppEnvironment Env { get; private set; }

        protected static readonly ILog Logger = LogManager.GetLogger(typeof(BaseTests));
        #endregion

        #region NUnit Functions

        [OneTimeSetUp] // one time before each execution of Test Suite
        public void OneTimeBrowserSetUp()
        {
            Console.WriteLine("Inside OneTimeSetUp");
            AssemblyPath = Framework.GetAssemblyPath();
            Env = Framework.GetAppEnvironment(EnvName);
            BrowserActions.KillChromeDriver();
        }

        [SetUp] // one time before each test
        public void SetUp()
        {
            Console.WriteLine("Inside SetUp");
            Console.WriteLine("Starting Test named: " + TestContext.CurrentContext.Test.MethodName);

            BasicConfigurator.Configure();

            var chromeService = ChromeDriverService.CreateDefaultService(AssemblyPath);
            Driver = new ChromeDriver(chromeService);
            ManageDriver();
            var baseMapPage = new BaseMapPage(Driver, Logger);
            var assertionMsg = $"Successfully navigate Browser to {Env.AppUrl}";
            Assert.DoesNotThrow(() => baseMapPage.NavigateUrl(Env.AppUrl), assertionMsg);
            Logger.Info(assertionMsg);
            var cookiesDialog = new CookiesDialog(Driver, Logger);
            if (cookiesDialog.IsLoaded())
            {
                cookiesDialog.AcceptCookies();
                Logger.Info("Accepted all Google cookies");
            }
        }

        [TearDown]
        public void TearDown()
        {
            Logger.Debug("Inside TearDown");
            Driver.Close();
            BrowserActions.KillChromeDriver();
        }
        #endregion

        #region Private functions

        private void LoggingTests()
        {
            XmlConfigurator.Configure();
        }
        
        private void ManageDriver()
        {
            var myTimeout = Driver.Manage().Timeouts();
            myTimeout.ImplicitWait = TimeSpan.FromSeconds(Env.ElementTimeout);  // This should be also be a Constant value

            Driver.Manage().Window.Maximize();
            var sizeWnd = Driver.Manage().Window.Size;
            Logger.Debug($"Browser maximized; Size x={sizeWnd.Width}, y={sizeWnd.Height}");
        }

        #endregion
    }
}