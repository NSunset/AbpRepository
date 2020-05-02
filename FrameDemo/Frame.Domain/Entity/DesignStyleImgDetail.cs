using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Design_StyleImgDetail")]
    public class DesignStyleImgDetail: FullAuditedEntity<long>
    {
        /// <summary>
        /// 款式Id
        /// </summary>
        public long StyleId { get; set; }

        /// <summary>
        /// 类型，0：图片，1：款式-展示图，2：款式-单图
        /// </summary>
        public ImgDetailType Type { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 图片顺序，图片与款式-展示图都为0
        /// </summary>
        public int Sort { get; set; }

    }
}
