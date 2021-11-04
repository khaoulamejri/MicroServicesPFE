using Compank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compank.Services
{
    public interface ICompanyServices
    {
        List<Companyk> GetAllCompany();
        Companyk GetCompanyByID(int id);
        int GetCurrentCompanyID();
        Companyk Create(Companyk Company);
        Companyk Edit(Companyk Company);
        Companyk Delete(int CompanyId);
        //  List<SelectListItem> GetAllCompanyDropDownList();
        Companyk GetCompanyByName(string name);
        List<Companyk> GetCompaniesByUser(string UserId);
        bool CheckUnicityCompanyByName(string name);
        bool CheckUnicityCompanyByNameID(string name, int ID);
    }
}
