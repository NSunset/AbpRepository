using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Manage_Permission")]
    public class ManagePermission:Entity<long>
    {
        private long fatherId;
        [StringLength(100)]
        public string PerName { get; set; }

        [StringLength(200)]
        public string PerValue { get; set; }

        public long FatherId { get; set; }

        /// <summary>
        /// 1菜单权限，2系统权限
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 菜单Ioc
        /// </summary>
        [StringLength(100)]
        public string Ioc { get; set; }
    }
}
