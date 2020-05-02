using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Frame.Domain
{
    [Table("Manage_User_Role")]
    public class ManageUserRole:Entity<long>
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }
}
