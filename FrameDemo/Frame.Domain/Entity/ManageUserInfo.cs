using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Manage_UserInfo")]
    public class ManageUserInfo : FullAuditedEntity<long>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50)]
        public string Passwd { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public string RoleName { get; set; }
    }
}
