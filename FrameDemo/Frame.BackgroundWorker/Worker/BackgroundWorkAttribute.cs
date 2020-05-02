using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.BackgroundWorker
{
    /// <summary>
    /// 后台工作者具体实现应该打上此标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class BackgroundWorkAttribute : Attribute
    {
    }
}
