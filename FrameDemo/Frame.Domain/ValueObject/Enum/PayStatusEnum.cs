using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frame.Domain
{
    /// <summary>
    /// 支付状态：0默认、1待支付、2支付中、3支付失败
    /// </summary>
    public enum PayStatusEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Payment = 0,

        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        ToBePaid = 1,

        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Paid = 2,

        /// <summary>
        /// 支付失败
        /// </summary>
        [Description("支付失败")]
        PayFail=3,
        /// <summary>
        /// 支付中
        /// </summary>
        [Description("支付中")]
        InPayment = 4
    }
}
