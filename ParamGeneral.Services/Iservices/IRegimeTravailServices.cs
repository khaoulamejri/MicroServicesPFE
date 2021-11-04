using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
  public  interface IRegimeTravailServices
    {
        List<RegimeTravail> GetAllRegimeTravail();
        RegimeTravail Create(RegimeTravail regimeTravail);
        RegimeTravail GetRegimeTravailByID(int id);
        RegimeTravail GetRegimTravailByID(int? id);
        RegimeTravail Edit(RegimeTravail regimeTravail);
        RegimeTravail Delete(int regimeTravailId);
      List<SelectListItem> SelectListItemRegimeTravail();
        bool CheckRegimeTravailExist(string intitule);
        bool CheckRegimeTravailUnicity(string code);
    }
}
