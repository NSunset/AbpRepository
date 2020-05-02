using Abp.Threading.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    public class PeriodicWorkerPxoxy : IBackgroudWorkerProxy
    {
        protected readonly AbpTimer timer;
        protected Action exceteAction;
        public PeriodicWorkerPxoxy(AbpTimer timer)
        {
            this.timer = timer;
            this.timer.Elapsed += Timer_Elapsed;
        }


        private void Timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {

            }
        }

        public void Excete<T>(Action method, WorkerConfigAbs config) where T : IBackgroundWorkerDo
        {
            exceteAction = method;
            timer.Period = config.IntervalSecond * 1000;//abpTime以毫秒为单位
            timer.Start();
        }

        public virtual void DoWork()
        {
            exceteAction();
        }
    }
}
