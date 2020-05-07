using Abp.Dependency;
using Frame.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 后台调度
    /// </summary>
    [BackgroundWork]
    public class SmsWorker : Frame.BackgroundWorker.BackgroundWorker<SmsWorker>, ISingletonDependency
    {
        public SmsWorker(IBackgroudWorkerProxy backgroudWorkerProxy) : base(backgroudWorkerProxy, new WorkerConfig { IntervalSecond = 10, WorkerId = "smsWorker" })
        {
        }

        public override void DoWork()
        {
            AppConfigurationServices.LogHelp.Info($"短信调度执行中。。。。发送短信内容{Guid.NewGuid().ToString()}");
        }
    }
}
