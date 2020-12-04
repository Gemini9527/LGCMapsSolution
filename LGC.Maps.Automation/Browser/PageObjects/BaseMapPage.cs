using LGC.AutoFramework.Browser;
using LGC.Maps.Automation.Browser.Widgets;
using log4net;
using OpenQA.Selenium;

namespace LGC.Maps.Automation.Browser.PageObjects
{
    public class BaseMapPage : AbstractPage
    {
        #region Constructor
        public BaseMapPage(IWebDriver driver, ILog log) : base(driver, log)
        {

        }
        #endregion

        #region Page Actions
        public override bool IsLoaded()
        {
            var searchWidget = new GoogleSearch(Driver, Log);
            var mapsLeftPanel = new MapsLeftPanel(Driver, Log);
            return searchWidget.IsLoaded() || mapsLeftPanel.IsLoaded();
        }
        #endregion
    }
}
