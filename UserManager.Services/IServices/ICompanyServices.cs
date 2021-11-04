using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface ICompanyServices
    {
        List<Company> GetAllCompany();
        Company GetCompanyByID(int id);
        int GetCurrentCompanyID();
        Company Create(Company Company);
        Company Edit(Company Company);
        Company Delete(int CompanyId);
        List<SelectListItem> GetAllCompanyDropDownList();
        Company GetCompanyByName(string name);
        List<Company> GetCompaniesByUser(string UserId);
        bool CheckUnicityCompanyByName(string name);
        bool CheckUnicityCompanyByNameID(string name, int ID);
    }
}
