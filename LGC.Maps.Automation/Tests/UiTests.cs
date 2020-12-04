using LGC.Maps.Automation.Browser.Widgets;
using NUnit.Framework;

namespace LGC.Maps.Automation.Tests
{
    [TestFixture]
    class UiTests : BaseTests
    {
        [TestCase("Dublin")]
        [TestCase("Cork")]
        public void VerifyMapsDestination(string destination)
        {
            var searchWidget = new GoogleSearch(Driver, Logger);
            var assertionMsg = "Search Widget is Loaded on Maps Landing Page";
            Assert.IsTrue(searchWidget.IsLoaded(), assertionMsg);
            Logger.Info(assertionMsg);
            assertionMsg = $"Successfully set search = {destination} and Verify Header Title on Maps Search Widget";
            Assert.DoesNotThrow(() => searchWidget.SetInitialSearchData(destination).VerifyHeaderTitle(destination), assertionMsg);
            Logger.Info(assertionMsg);
            assertionMsg = $"Successfully Clicked Directions Icon And Verified Final Destination {destination}";
            Assert.DoesNotThrow(() => new MapsLeftPanel(Driver, Logger).ClickDirectionsIconAndVerifyFinalDestination(destination), assertionMsg);
            Logger.Info(assertionMsg);
        }
    }
}
