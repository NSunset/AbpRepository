using Abp.Threading.BackgroundWorkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 所有后台工作者类都应该实现该接口
    /// </summary>
    public interface IBackgroundWorkerDo: IBackgroundWorker
    {
        /// <summary>
        /// 执行具体的任务
        /// </summary>
        void DoWork();
    }
}
