using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frame.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Manage_PayHistory")]
    public class ManagePayHistory : FullAuditedEntity<long>
    {
        /// <summary>
        /// 设计师用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 微信转账订单号
        /// </summary>
        public string WXOrderNumber { get; set; }

        /// <summary>
        /// 转账金额，含税
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实发金额，不含税
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 代扣税金额
        /// </summary>
        public decimal WithholdingTaxAmount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 包含订单数
        /// </summary>
        public int IncludeOrderNum { get; set; }

        /// <summary>
        /// 支付状态，0：失败，1：成功
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 设计师
        /// </summary>
        [ForeignKey("UserId")]
        public virtual DesignUserInfo DesignUserInfo { get; set; }


        /// <summary>
        /// ManagePayDetail
        /// </summary>
        [ForeignKey("ManagePayHistoryId")]
        public virtual IList<ManagePayDetail> ManagePayDetailList { get; set; }
    }
}
