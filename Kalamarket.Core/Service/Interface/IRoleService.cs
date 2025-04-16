using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface IRoleService
    {
        bool CheckPermission(int userid, int permissionid);
    }
}
