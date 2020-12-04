using System;
using System.IO;
using Newtonsoft.Json;

namespace LGC.AutoFramework.Helper
{
    public class Framework
    {
        public static AppEnvironment GetAppEnvironment(string envName)
        {
            var globalJsonPath = Path.Combine(GetAssemblyPath(), "Environments", "Global.json");
            using (StreamReader file = File.OpenText(globalJsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                AppEnvironmentRoot env = (AppEnvironmentRoot)serializer.Deserialize(file, typeof(AppEnvironmentRoot));
                if (env == null)
                {
                    throw new Exception($"Unable to read Global variable json at:{globalJsonPath}");
                }
                return env.AppEnvironments.Find(a => a.Environment.ToLower() == envName.ToLower());
            }
        }

        public static string GetAssemblyPath()
        {
            var assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            if (string.IsNullOrEmpty(assemblyPath))
            {
                throw new Exception("Unable to get assembly bin Path path to search ChromeDriver.exe");
            }
            return assemblyPath.Substring(6);
        }
    }
}
