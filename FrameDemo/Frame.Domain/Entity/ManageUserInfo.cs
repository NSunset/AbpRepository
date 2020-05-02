using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
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
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Passwd { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public ICollection<ManageUserRole> ManageUserRoles { get; set; }
    }
}
