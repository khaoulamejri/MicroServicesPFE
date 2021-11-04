using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IUserServices
    {
        ApplicationUser Edit(ApplicationUser user);
        ApplicationUser GetUserByID(string userId);
        List<ApplicationUser> GetAllUsers();
        List<SelectListItem> GetAllLanguages();
        ApplicationUser GetUserByUserName();
        ApplicationUser GetUserByUserName(string UserName);
        bool GetRoleUserByUserName();
        string CheckMatriculeEmployee(ApplicationUser user);
        ApplicationUser GetUserByEmail(string email);
        List<UserViewModel> GetFullUsers();
        string GetUserName();
        int GetNBActifUsers();
    }
}
