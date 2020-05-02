using System;
using System.Collections.Generic;
using System.Text;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.Timers;
using Frame.BackgroundWorker;
using Frame.Common;

namespace Frame.Domain.Sms
{
    [BackgroundWork]
    public class SmsWorker : Frame.BackgroundWorker.BackgroundWorker<SmsWorker>, ISingletonDependency
    {
        public SmsWorker(IBackgroudWorkerProxy backgroudWorkerProxy) : base(backgroudWorkerProxy, new WorkerConfig { IntervalSecond = 10, WorkerId = "smsWorker" })
        {
        }

        public override void DoWork()
        {
            AppConfigurationServices.LogHelp.Info($"短信调度执行中。。。。发型短信内容{Guid.NewGuid().ToString()}");
        }
    }

    public class SimpleSendSmsJob : BackgroundJob<int>, ITransientDependency
    {
        [UnitOfWork]
        public override void Execute(int args)
        {
            Logger.Info($"后台作业从队列中执行任务{args}:{DateTime.Now.ToString()}");
        }
    }

    //public class SmsWorkerPeriodicWorkerPxoxy : PeriodicWorkerPxoxy
    //{
    //    public SmsWorkerPeriodicWorkerPxoxy(AbpTimer timer) : base(timer)
    //    {
    //    }

    //    public override void DoWork()
    //    {
    //        AppConfigurationServices.LogHelp.Info($"短信调度执行中。。。。开始{111}");
    //        base.DoWork();
    //        AppConfigurationServices.LogHelp.Info($"短信调度执行中。。。。结束{222}");
    //    }
    //}
}
