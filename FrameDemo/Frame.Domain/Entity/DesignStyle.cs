using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Frame.Domain
{
    [Table("Design_Style")]
    public class DesignStyle : FullAuditedEntity<long>
    {
        /// <summary>
        /// 款式订单号，提交就自动生成
        /// </summary>
        public string StyleOrderNumber { get; set; }

        /// <summary>
        /// 设计师用户Id
        /// </summary>
        public long UserId { get; set; }

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
        /// 款式名称
        /// </summary>
        public string StyleName { get; set; }

        /// <summary>
        /// 审核状态，0：待审核，1：已通过，2：未通过
        /// </summary>
        public StyleAuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 支付状态，0：初始值，1：待支付，2：已支付,3支付失败
        /// </summary>
        public PayStatusEnum PayStatus { get; set; }

        /// <summary>
        /// 未通过原因
        /// </summary>
        public string RefuseMsg { get; set; }

        /// <summary>
        /// 数据版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 款式图片详情
        /// </summary>
        [ForeignKey("StyleId")]
        public virtual IList<DesignStyleImgDetail> DesignStyleImgDetail { get; set; }

        /// <summary>
        /// 设计师
        /// </summary>
        [ForeignKey("UserId")]
        public virtual DesignUserInfo DesignUserInfo { get; set; }

        /// <summary>
        /// 款式生命线
        /// </summary>
        [ForeignKey("StyleId")]
        public virtual IList<DesignStyleLifeLine> DesignStyleLifeLine { get; set; }

        /// <summary>
        /// Design_StyleCategory
        /// </summary>
        [ForeignKey("StyleId")]
        public virtual IList<DesignStyleCategory> DesignStyleCategory { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }
    }
}
