using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frame.Domain
{
    public enum StyleAuditStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        PendingReview = 0,

        /// <summary>
        /// 已通过
        /// </summary>
        [Description("已通过")]
        Passed = 1,

        /// <summary>
        /// 未通过
        /// </summary>
        [Description("未通过")]
        DidNotPass = 2
    }
}
