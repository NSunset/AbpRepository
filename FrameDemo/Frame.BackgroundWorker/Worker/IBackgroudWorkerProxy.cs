using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    public interface IBackgroudWorkerProxy
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="method"></param>
        void Excete<T>(Action method, WorkerConfigAbs config) where T : IBackgroundWorkerDo;
    }
}
