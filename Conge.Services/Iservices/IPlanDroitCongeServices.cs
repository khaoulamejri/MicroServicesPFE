using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IPlanDroitCongeServices
    {
        List<PlanDroitConge> GetAllPlanDroitConge();
        PlanDroitConge GetPlanDroitCongeByID(int id);
        PlanDroitConge GetPlanDroitCongeById(int id);
        PlanDroitConge Create(PlanDroitConge PlanDroitConge);
        PlanDroitConge Edit(PlanDroitConge PlanDroitConge);
        PlanDroitConge Delete(int PlanDroitCongeId);
        // List<SelectListItem> SelectListItemPlanDroitConge();
        bool checkUnicity(PlanDroitConge PlanDroitConge);

    }
}
