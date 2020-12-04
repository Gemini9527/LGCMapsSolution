using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LGC.AutoFramework.Helper
{
    public class AppEnvironment
    {
        public string Environment { get; set; }
        public string AppUrl { get; set; }
    }

    public class AppEnvironmentRoot
    {
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<AppEnvironment> AppEnvironments { get; set; }
    }
}
