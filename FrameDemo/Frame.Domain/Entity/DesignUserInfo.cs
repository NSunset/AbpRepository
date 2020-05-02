using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Design_UserInfo")]
    public class DesignUserInfo : FullAuditedEntity<long>
    {
        /// <summary>
        /// 微信的OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 身份证正面地址
        /// </summary>
        public string IdCardFront { get; set; }

        /// <summary>
        /// 身份证反面地址
        /// </summary>
        public string IdCardReverse { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否校验过身份证
        /// </summary>
        public string IsValidIdCard { get; set; }
    }
}
