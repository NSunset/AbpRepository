using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Logging;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Frame.Common
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppConfigurationServices
    {
        private static ILogger loghelp;
        public static IConfiguration Configuration { get; private set; } = null;

        public static ILogger LogHelp { get; private set; }

        private AppConfigurationServices() { }

        static AppConfigurationServices()
        {
            SetConfig();
            Log4Config();
        }

        public static void Log4Config()
        {
            var iocManager = Abp.Dependency.IocManager.Instance;
            if (!iocManager.IsRegistered(typeof(ILoggerFactory)))
            {
                iocManager.IocContainer.AddFacility<Castle.Facilities.Logging.LoggingFacility>(f => f.UseAbpLog4Net().WithConfig(Path.Combine(CalculateContentRootFolder(), "log4net.config")));
            }
            LogHelp = Abp.Logging.LogHelper.Logger;
        }



        public static void SetConfig(IConfiguration configuration = null)
        {
            if (configuration == null)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(CalculateContentRootFolder())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                Configuration = builder.Build();
            }
            else
            {
                Configuration = configuration;
            }
        }

        private static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(FrameCommonModel).Assembly.Location);
#if DEBUG
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of Frame.Common assembly!");
            }
            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "FrameDemo.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }
            var webHostFolder = Path.Combine(directoryInfo.FullName, "Frame.Mvc");
            if (Directory.Exists(webHostFolder))
            {
                return webHostFolder;
            }
            throw new Exception("Could not find root folder of the web project!");
#endif
            return coreAssemblyDirectoryPath;
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
