using LGC.AutoFramework.Browser;
using LGC.Maps.Automation.Browser.PageObjects;
using log4net;
using OpenQA.Selenium;

namespace LGC.Maps.Automation.Browser.Widgets
{
    public class CookiesDialog : AbstractWidget
    {
        #region Constructor
        public CookiesDialog(IWebDriver driver, ILog log) : base(driver, log)
        {

        }
        #endregion

        #region Selectors
        private By Frame => By.XPath("//iframe[@class='widget-consent-frame']");
        private By SeeMore => By.XPath("//*[contains(text(),'See more')]");
        private By IAgreeButton => By.XPath("//div[@id='introAgreeButton']");
        #endregion

        #region Page Actions

        public override bool IsLoaded()
        {
            return WaitForLoad(Frame) != null;
        }
        
        public BaseMapPage AcceptCookies()
        {
            var dialogFrame = Driver.FindElement(Frame);
            Driver.SwitchTo().Frame(dialogFrame);
            var acceptButton = Driver.FindElement(IAgreeButton);
            ClickOnElement(acceptButton);
            Log.Info($"AcceptCookies");
            return new BaseMapPage(Driver, Log);
        }
        #endregion
    }
}