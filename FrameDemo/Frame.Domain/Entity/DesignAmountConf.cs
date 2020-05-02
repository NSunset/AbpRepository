using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Design_AmountConf")]
    public class DesignAmountConf : FullAuditedEntity<long>
    {
        /// <summary>
        /// 图片种类，1：款式，2：图片
        /// </summary>
        [Column("Image_Kind")]
        public StyleImageKindType ImageKind { get; set; }

        /// <summary>
        /// 图片类型，1：原创，2：素材
        /// </summary>
        [Column("Image_Type")]
        public StyleImageType ImageType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
