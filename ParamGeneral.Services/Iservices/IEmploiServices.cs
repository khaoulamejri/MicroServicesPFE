using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
  public  interface IEmploiServices
    {
        List<Emploi> GetAllEmploi();
        Emploi Create(Emploi Emploi);
        Emploi Edit(Emploi Emploi);
        Emploi Delete(int EmploiId);
        Emploi GetEmploiById(int EmploiId);
        List<Emploi> GetAllEmploiByCompany(int CompanyId);
        List<Emploi> GetAllEmploiByKey(string key);
        bool checkUnicity(Emploi Emploi, bool create);
    }
}
