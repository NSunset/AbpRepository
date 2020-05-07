using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("user_info")]
    public class UserInfo : FullAuditedEntity<long>
    {
        /// <summary>
        /// 类型，1：微信
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 标识Key
        /// </summary>
        [StringLength(50)]
        public string IdentityKey { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(100)]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(100)]
        public string AvatarUrl { get; set; }
        [NotMapped]
        public string ShowNickName
        {
            get
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(NickName));
            }
        }
    }
}
