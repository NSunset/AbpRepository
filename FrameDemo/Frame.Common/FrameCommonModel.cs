using Abp.Castle.Logging.Log4Net;
using Abp.Modules;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
namespace Frame.Common
{
    
    public class FrameCommonModel : AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameCommonModel).Assembly);
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
