using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IUserRoleCompaniesServices
    {
        bool CheckUnicityUserRoleCompanies(int companyID, string UserID, string roleID);
        ApplicationUserRoleCompanies create(ApplicationUserRoleCompanies UserRoleCompanies);
        List<ApplicationUserRoleCompanies> GetAllUserRoleCompaniesByUserCompanyID(string userID, int companyID);
        List<ApplicationUserRoleCompanies> GetAllUserRoleCompaniesByUserID(string userID);
        ApplicationUserRoleCompanies Delete(ApplicationUserRoleCompanies UserRoleCompanies);
    }
}
