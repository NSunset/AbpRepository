using Hangfire;
using Hangfire.MySql.Core;
using Hangfire.Server;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker.Hangfire
{
    public static class HangifeConfigureServices
    {
        static HangifeConfigureServices()
        {
            BackgroundWorkManagerConfig.IsEnableHangfire = true;
        }

        /// <summary>
        /// Hangfire的ConfigureServices配置
        /// </summary>
        /// <typeparam name="TStorage"></typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="compatibilityLevel">相容性等级</param>
        /// <param name="storage">存储空间</param>
        public static void AddHangfire<TStorage>(this IServiceCollection services, [NotNull] TStorage storage, CompatibilityLevel compatibilityLevel = CompatibilityLevel.Version_170) where TStorage : JobStorage
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(compatibilityLevel)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(storage);
            });
        }

        /// <summary>
        /// Hangfire的Configure配置
        /// </summary>
        /// <param name="app">应用程序生成器</param>
        /// <param name="options">HangfireServer 后台作业服务器选项</param>
        /// <param name="additionalProcesses">HangfireServer 附加过程</param>
        /// <param name="storage">HangfireServer 作业存储</param>
        /// <param name="pathMatch">UseHangfireDashboard 路径</param>
        /// <param name="dashboardOptions">UseHangfireDashboard 后台作业服务器选项</param>
        /// <param name="dashboardStorage">UseHangfireDashboard 作业存储</param>
        public static void HangfireConfigure(this IApplicationBuilder app, [CanBeNull] BackgroundJobServerOptions options = null, [CanBeNull] IEnumerable<IBackgroundProcess> additionalProcesses = null, [CanBeNull] JobStorage storage = null, [NotNull] string pathMatch = "/hangfire", [CanBeNull] DashboardOptions dashboardOptions = null, [CanBeNull] JobStorage dashboardStorage = null)
        {
            app.UseHangfireServer(options, additionalProcesses, storage);
            app.UseHangfireDashboard(pathMatch, dashboardOptions, dashboardStorage);
        }
    }
}
