using LGC.AutoFramework.Browser;
using log4net;
using OpenQA.Selenium;

namespace LGC.Maps.Automation.Browser.Widgets
{
    class GoogleSearch : AbstractWidget
    {
        #region Constructor
        public GoogleSearch(IWebDriver driver, ILog log) : base(driver, log)
        {

        }
        #endregion

        #region Selectors
        private By SearchBoxInput => By.XPath("//input[@id='searchboxinput']");
        private By SearchBoxButton => By.XPath("//button[@id='searchbox-searchbutton']");
        #endregion

        #region Page Actions
        public override bool IsLoaded()
        {
            WaitForLoad(SearchBoxInput);
            return Driver.FindElement(SearchBoxInput).Displayed;
        }

        public MapsLeftPanel SetInitialSearchData(string searchValue)
        {
            var searchInput = Driver.FindElement(SearchBoxInput);
            ClickOnElement(searchInput);
            SetTextOnElement(searchInput, searchValue);
            var searchButton = Driver.FindElement(SearchBoxButton);
            WaitForLoad(SearchBoxButton);
            ClickOnElement(searchButton);
            Log.Info($"SetInitialSearchData, value={searchValue}");
            return new MapsLeftPanel(Driver, Log);
        }
        #endregion
    }
}