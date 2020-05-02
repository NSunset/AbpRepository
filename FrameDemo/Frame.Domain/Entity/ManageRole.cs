using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Manage_Role")]
    public class ManageRole:Entity<long>
    {
        public string RoleName { get; set; }

        public int Sort { get; set; }

        public long FatherId { get; set; }

        public bool IsEnable { get; set; }

        public ICollection<ManageRolePermission> ManageRolePermissions { get; set; }
    }
}
