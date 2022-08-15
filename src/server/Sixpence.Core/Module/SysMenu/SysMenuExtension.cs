using Sixpence.Core.Auth;
using Sixpence.Core.Auth.Privilege;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixpence.Core.Module.SysMenu
{
    public static class SysMenuExtension
    {
        public static IEnumerable<sys_menu> Filter(this IEnumerable<sys_menu> sysMenus)
        {
            var privileges = UserPrivilegesCache.GetUserPrivileges(UserIdentityUtil.GetCurrentUserId()).Where(item => item.object_type == nameof(sys_menu));
            return sysMenus.Where(item =>
            {
                var data = privileges.FirstOrDefault(e => e.objectid == item.id);
                return data != null && data.privilege > 0;
            });
        }
    }
}
