using LGC.AutoFramework.Browser;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LGC.Maps.Automation.Browser.Widgets
{
    class MapsLeftPanel : AbstractWidget
    {
        public MapsLeftPanel(IWebDriver driver, ILog log) : base(driver, log)
        {

        }

        #region Selectors
        private string _destinationXpathString = "//div[@id='directions-searchbox-{destinationNumber}']//input";
        private By HeaderTitle => By.XPath("//div[@class='section-hero-header-title-top-container']//h1");
        private By DirectionsIcon => By.XPath("//img[@alt='Directions']");
        #endregion

        #region Page Actions

        public override bool IsLoaded()
        {
            WaitForLoad(HeaderTitle);
            return Driver.FindElement(HeaderTitle).Displayed;
        }

        public MapsLeftPanel VerifyHeaderTitle(string expectedTitle)
        {
            IsLoaded();
            var headerTitleElem = Driver.FindElement(HeaderTitle);
            var actualTitle = GetTextOfElement(headerTitleElem);
            Assert.AreEqual(expectedTitle.ToLower(), actualTitle.ToLower(), "VerifyHeaderTitle");
            Log.Info("VerifyHeaderTitle");
            return this;
        }

        public MapsLeftPanel ClickDirectionsIconAndVerifyFinalDestination(string expectedDestination)
        {
            IsLoaded();
            var directionsIcon = Driver.FindElement(DirectionsIcon);
            ClickOnElement(directionsIcon);
            Log.Info("ClickDirectionsIconAndVerifyFinalDestination");
            return VerifyDestinationTextByDestIndex(1, expectedDestination);
        }

        public MapsLeftPanel VerifyDestinationTextByDestIndex(byte destinationNumber, string expectedDestination)
        {
            _destinationXpathString = _destinationXpathString.Replace("{destinationNumber}", destinationNumber.ToString());
            By byDestination = By.XPath(_destinationXpathString);
            WaitForLoad(byDestination);
            var destination = Driver.FindElement(byDestination);
            var actualText = GetAttributeOfElement(destination, "aria-label").Replace("Destination", "").Trim();
            Assert.AreEqual(expectedDestination.ToLower(), actualText.ToLower(), "VerifyHeaderTitle");
            Log.Debug($"VerifyDestinationTextByDestIndex, Actual Text={actualText}");
            return this;
        }
        #endregion
    }
}