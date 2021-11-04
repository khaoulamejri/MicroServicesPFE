using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
  public  interface IDepartementServices
    {
        List<Departement> GetAllDepartements();
        Departement GetDepartementByID(int id);
        Departement Create(Departement Departement);
        Departement Edit(Departement Departement);
        Departement Delete(int DepartementId);
      List<SelectListItem> SelectListItemDepartements();
        bool CheckUnicityDepartement(string code);
        bool CheckUnicityDepartementByID(string code, int ID);
    }
}
