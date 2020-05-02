using Abp.AspNetCore.Configuration;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Frame.Application;
using Frame.EntityFrameworkCore;
using Frame.BackgroundWorker.Hangfire;
using Abp.Threading.BackgroundWorkers;
using Frame.Domain.Sms;
using Frame.BackgroundWorker;
using Abp.Dependency;
using Abp.AspNetCore.SignalR;
using Abp.EntityFrameworkCore;

namespace Frame.Mvc
{
    [DependsOn(
        typeof(FrameApplicationModel),
        typeof(FrameEFCoreModel),
        typeof(Abp.AspNetCore.AbpAspNetCoreModule),
        typeof(AbpHangfireAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule))]
    public class FrameMvcModel:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(FrameApplicationModel).Assembly);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameMvcModel).Assembly);
        }
    }
}
