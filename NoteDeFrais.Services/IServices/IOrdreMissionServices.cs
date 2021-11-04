using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IOrdreMissionServices
    {
        OrdreMission GetOrdreMissionById(int id);
        List<OrdreMission> GetAllOrdreMission();
        OrdreMission Create(OrdreMission ordreMission);
        OrdreMission Edit(OrdreMission ordreMission);
        OrdreMission Delete(int ordreMissionId);
        List<OrdreMissionVM> GetOrdreMissionByEmployeeId(int employeeId, int currentCompanyId);
        List<OrdreMission> GetOrdreMissionForOthers(string username, int employeeId, int companyId);
        bool checkTitleUnicity(OrdreMission ordreMission);
        List<OrdreMissionVM> getValidatedMissionOrdersByEmployeeId(int employeeId);
        List<OrdreMissionVM> getMissionOrdersByType(int missionOrderType);
        bool checkNoteDates(NotesFrais notesFrais);
        bool checkPeriodUnicity(OrdreMission ordreMission);
        string checkMiisonOrdersAndLeaveDates(int employeeId, System.DateTime dateDebutConge, System.DateTime dateRepriseConge);
        OrdreMission GetAllIncludedOrdreMissionById(int id);
        OrdreMissionVM ConvertToOrdreMissionVM(OrdreMission ordreMission);

    }
}
