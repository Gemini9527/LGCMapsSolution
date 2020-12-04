using log4net;
using OpenQA.Selenium;

namespace LGC.AutoFramework.Browser
{
    public abstract class AbstractPage : AbstractBrowser
    {
        public AbstractPage(IWebDriver driver, ILog log) : base(driver, log)
        {

        }
        public AbstractPage NavigateUrl(string url)
        {
            Driver.Url = url;
            return this;
        }
    }
}
