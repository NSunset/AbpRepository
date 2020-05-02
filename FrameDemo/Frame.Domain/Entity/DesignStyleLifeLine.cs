using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Design_StyleLifeLine")]
    public class DesignStyleLifeLine : FullAuditedEntity<long>
    {
        /// <summary>
        /// 款式Id
        /// </summary>
        public long StyleId { get; set; }

        /// <summary>
        /// 生命线状态Id
        /// </summary>
        public long LifeStatusId { get; set; }

        /// <summary>
        /// 该过程的结果
        /// </summary>
        public bool ActionResult { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否是最后的动作
        /// </summary>
        public bool IsLastAction { get; set; }

    }
}
