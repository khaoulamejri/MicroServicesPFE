using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IUserRolesServices
    {
        //List<SelectListItem> GetAllRoles();
        List<ApplicationRole> GetRoles();
        int GetRolesNumber(string name);
        ApplicationRole GetRoleById(string id);
        ApplicationRole GetRoleByName(string name);
        ApplicationRole AddRole(ApplicationRole role);
        ApplicationRole UpdateRole(ApplicationRole role);
        ApplicationRole DeleteRole(string name);
        bool IsRoleAssigned(string name);
        bool IsSysAdmin();
        string GetRoleName(string name, string id, int cmpId);
        List<string> getListRolesNames(string id, int cmpId);
        int GetRoleNameLength(string name, int cmpId, string UserName);
        List<Object> GetRoleGrouppedByUserId(string id, List<ApplicationRole> rolesList, List<Company> companiesList);
    }
}
