using Abp.Dependency;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Castle.MicroKernel.Registration;
using Frame.BackgroundWorker;
using Frame.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.Domain
{
    [DependsOn(typeof(Frame.Common.FrameCommonModel), typeof(FrameBackgroundWorkerModel))]
    public class FrameDomainModel : AbpModule
    {
        private readonly IConfiguration configuration;
        public FrameDomainModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public override void PreInitialize()
        {
            if (!bool.Parse(configuration["BackgroundServer:Job:Enable"]))
            {
                Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            if (bool.Parse(configuration["BackgroundServer:Worker:Enable"]))
            {
                BackgroundWorkManagerConfig.Config<FrameBackgroundWorkerModel>(IocManager);
            }
        }
    }
}
