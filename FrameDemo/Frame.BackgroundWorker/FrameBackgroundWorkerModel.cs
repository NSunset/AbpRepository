using Abp.Dependency;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Hangfire;

namespace Frame.BackgroundWorker
{
    [DependsOn(typeof(AbpHangfireAspNetCoreModule))]
    public class FrameBackgroundWorkerModel:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameBackgroundWorkerModel).Assembly);
        }

        public override void PreInitialize()
        {
            if (BackgroundWorkManagerConfig.IsEnableHangfire)
            {
                IocManager.Register<IBackgroudWorkerProxy, HangfireWorkerPxoxy>();
                Configuration.BackgroundJobs.UseHangfire();
            }
            else
            {
                IocManager.Register<IBackgroudWorkerProxy, PeriodicWorkerPxoxy>();
            }
        }


    }
}
