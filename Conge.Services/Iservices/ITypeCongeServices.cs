using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface ITypeCongeServices
    {
        List<TypeConge> GetAllTypeConges();
        TypeConge GetTypeCongetByID(int id);
        TypeConge Create(TypeConge TypeConge);
        TypeConge Edit(TypeConge TypeConge);
        TypeConge Delete(int TypeCongeId);
   List<SelectListItem> SelectListItemTypeConge();
        TypeConge GetTypeCongetAnnuel();
        //bool CheckSameTypeConge(TypeCongeViewModel TypeConge);
        //bool CheckSameAnnualLeave(TypeCongeViewModel TypeConge);
        bool CheckSameTypeConge(TypeCongeViewModel TypeConge);
        bool CheckSameAnnualLeave(TypeCongeViewModel TypeConge);
       // Company CreateCompany(Company company);
    }
}
