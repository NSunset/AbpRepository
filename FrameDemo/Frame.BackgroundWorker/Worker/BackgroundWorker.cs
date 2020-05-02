using Abp.Threading.BackgroundWorkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 所有后台工作者类都应该集成此类
    /// </summary>
    public abstract class BackgroundWorker<T>: BackgroundWorkerBase, IBackgroundWorkerDo where T:IBackgroundWorkerDo
    {
        protected readonly IBackgroudWorkerProxy backgroudWorkerProxy;
        protected readonly WorkerConfigAbs workerConfigAbs;
        protected BackgroundWorker(IBackgroudWorkerProxy backgroudWorkerProxy, WorkerConfigAbs workerConfigAbs)
        {
            this.backgroudWorkerProxy = backgroudWorkerProxy;
            this.workerConfigAbs = workerConfigAbs;
        }

        public abstract void DoWork();

        /// <summary>
        /// 任务启动
        /// </summary>
        public override void Start()
        {
            Logger.Debug("轮询任务启动");
            backgroudWorkerProxy.Excete<T>(DoWork, workerConfigAbs);
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void WaitToStop()
        {
            base.WaitToStop();
        }
    }
}
