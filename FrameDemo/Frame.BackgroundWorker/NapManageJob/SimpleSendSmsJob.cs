using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 后台作业
    /// </summary>
    public class SimpleSendSmsJob : BackgroundJob<int>, ITransientDependency
    {
        [UnitOfWork]
        public override void Execute(int args)
        {
            Logger.Info($"后台作业从队列中执行任务{args}:{DateTime.Now.ToString()}");
        }
    }
}
