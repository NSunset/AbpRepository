using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;

namespace Frame.Application
{
    [DependsOn(typeof(Abp.AspNetCore.AbpAspNetCoreModule))]
    public class FrameApplicationModel:AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameApplicationModel).Assembly);           
        }
    }
}
