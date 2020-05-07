using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frame.Domain
{
    public enum PermissionType
    {
        /// <summary>
        /// 菜单权限
        /// </summary>
        [Description("菜单权限")]
        MenuPermissions = 1,

        /// <summary>
        /// 系统权限
        /// </summary>
        [Description("系统权限")]
        SystemAuthority = 2,
    }
}
