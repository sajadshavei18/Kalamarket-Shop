using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class RoleService : IRoleService
    {
        private KalamarketContext _Context;
        public RoleService(KalamarketContext Context)
        {
            _Context = Context;
        }

        public bool CheckPermission(int userid, int permissionid)
        {
            var Rolid = _Context.UserRoles.Where(c => c.userid == userid)
                .Select(c => c.Roleid).ToList();

            if (!Rolid.Any())
                return false;


            List<int> RolPermission = _Context.RolePermissions
                .Where(p => p.Permissionid == permissionid).Select(p => p.Roleid).ToList();


            return RolPermission.Any(c => Rolid.Contains(c));

        }

    }
}
