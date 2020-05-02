using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    public abstract class WorkerConfigAbs
    {
        /// <summary>
        /// 轮询秒数
        /// </summary>
        public int IntervalSecond { get; set; }
        /// <summary>
        /// 工作唯一编号
        /// </summary>
        public string WorkerId { get; set; }
    }

    public class WorkerConfig: WorkerConfigAbs
    {
    }
}
