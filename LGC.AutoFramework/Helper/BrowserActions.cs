using System.Diagnostics;

namespace LGC.AutoFramework.Helper
{
    public class BrowserActions
    {
        private const string DriverExe = "chromedriver";
        public static void KillChromeDriver()
        {
            var processesChromeDriver = Process.GetProcessesByName(DriverExe);
            foreach (Process p in processesChromeDriver)
            {
                p.Kill();
            }
        }
    }
}