using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    public class HangfireWorkerPxoxy : IBackgroudWorkerProxy
    {
        private WorkerConfigAbs config { get; set; }
        public void Excete<T>(Action method, WorkerConfigAbs config) where T : IBackgroundWorkerDo
        {
            this.config = config;
            string worerId = config.WorkerId;
            string cron = Cron.MinuteInterval((int)Math.Ceiling((decimal)config.IntervalSecond / 60));
            RecurringJob.AddOrUpdate<T>(config.WorkerId, (t) => t.DoWork(), cron, TimeZoneInfo.Local);
            RecurringJob.Trigger(config.WorkerId);
        }
    }
}
