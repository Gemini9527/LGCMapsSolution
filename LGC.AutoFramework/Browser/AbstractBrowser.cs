using System;
using System.Threading;
using log4net;
using OpenQA.Selenium;

namespace LGC.AutoFramework.Browser
{
    public abstract class AbstractBrowser
    {
        #region Fields
        protected IWebDriver Driver;
        protected ILog Log;
        #endregion

        #region Constructor
        public AbstractBrowser(IWebDriver driver, ILog log)
        {
            Driver = driver;
            Log = log;
        }
        #endregion

        #region Functions
        public abstract bool IsLoaded();

        public IWebElement WaitForLoad(By bySelector, short timeoutSeconds = 90)
        {
            short retryFrequencySeconds = 2;
            for (short iCnt = 0; iCnt < timeoutSeconds; iCnt += retryFrequencySeconds)
            {
                try
                {
                    var elem = Driver.FindElement(bySelector);
                    return elem;
                }
                catch(Exception)
                {
                    Thread.Sleep(retryFrequencySeconds * 1000);
                }
            }
            Log.Debug($"WaitForDisplay failed for {bySelector}");
            return null;
        }

        public void ClickOnElement(IWebElement elem)
        {
            IsElementReady(elem);
            elem.Click();
        }

        public string GetTextOfElement(IWebElement elem)
        {
            IsElementReady(elem);
            Log.Debug($"GetText of element {elem} = {elem.Text}");
            return elem.Text;
        }

        public string GetAttributeOfElement(IWebElement elem, string attributeName)
        {
            IsElementReady(elem);
            Log.Debug($"GetAttribute of element {elem} = {elem.GetAttribute(attributeName)}");
            return elem.GetAttribute(attributeName);
        }

        public bool SetTextOnElement(IWebElement elem, string value, bool appendToValue = false)
        {
            IsElementReady(elem);
            Log.Debug($"SetText on element {elem}, Value={value}");
            if(!appendToValue)
            {
                elem.Clear();
            }
            elem.SendKeys(value);
            return true;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private bool IsElementReady(IWebElement elem)
        {
            if (!elem.Displayed)
            {
                throw new ElementNotVisibleException($"Elemet {elem} is not displayed");
            }
            if (!elem.Enabled)
            {
                throw new ElementNotInteractableException($"Elemet {elem} is not enabled");
            }
            return true;
        }
        #endregion
    }
}