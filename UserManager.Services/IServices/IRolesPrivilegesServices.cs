using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IRolesPrivilegesServices
    {
        RolesPrivileges create(RolesPrivileges RolesPrivileges);
        string GetRolesPrivilegesByRoleId(string roleID, string privilege);
        RolesPrivileges GetRolesPrivilegesByRoleIdPrivilege(string roleID, string privilege);
        RolesPrivileges Delete(RolesPrivileges RolesPrivileges);
        List<RolesPrivileges> GetAllRolesPrivilegesByRoleId(string roleID);
    }
}
