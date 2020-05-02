using Abp.Dependency;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.MySql;
using Hangfire.AspNetCore;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Hangfire.Dashboard;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 后台工作者管理配置
    /// </summary>
    public class BackgroundWorkManagerConfig
    {
        /// <summary>
        /// 是否使用Hangfire
        /// </summary>
        public static bool IsEnableHangfire = false;

        /// <summary>
        /// 把具体后台工作任务加入后台工作者管理列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iocManager"></param>
        public static void Config<T>(IIocManager iocManager) where T : AbpModule
        {
            var workManager = iocManager.Resolve<IBackgroundWorkerManager>();
            var types = typeof(T).Assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(typeof(BackgroundWorkAttribute), true).Length > 0)
                {
                    workManager.Add(iocManager.Resolve<IBackgroundWorker>(type));
                }
            }

        }
    }
}
