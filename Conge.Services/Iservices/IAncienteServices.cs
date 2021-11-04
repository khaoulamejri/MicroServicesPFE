using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
  public  interface IAncienteServices
    {
        Anciente GetAncienteByID(int id);
        List<Anciente> GetAncienteByPlanDroitCongeId(int id);
        List<Anciente> GetAncienteByPlanDroitCongeIdcmp(int id, int CompanyId);
        Anciente Create(Anciente Anciente);
        Anciente Edit(Anciente Anciente);
        string Delete(int AncienteId);
        bool checkUnicity(Anciente anciente, bool create);
    }
}
