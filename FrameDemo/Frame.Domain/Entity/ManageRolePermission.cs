using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Manage_Role_Permission")]
    public class ManageRolePermission:Entity<long>
    {
        public long PermissionId { get; set; }

        public long RoleId { get; set; }
    }
}
