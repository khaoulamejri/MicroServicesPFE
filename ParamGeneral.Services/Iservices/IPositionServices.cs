using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
   public interface IPositionServices
    {
        Position GetFirstPositionByEmployeeId(int employeeId);
     //   List<Position> GetAllPosition(bool includeUnit = true);
        List<Position> GetAllPosition();

        List<Position> GetAllPositionAllCompany();
        List<Position> GetAllPositionByCompanyId(int companyID);
        Position GetPositionByID(int id);
        List<Position> GetPositionByDepartementID(int id);
        Position Creat(Position Position);
        Position Edit(Position Position);
        Position Delete(int PositionId);
        List<Position> GetAllPositionByEmploiId(int emploiID);
        List<Position> GetAllPositionByReference(string reference);
       List<PositionViewModel> GetAllPositionByCompanyIdVM(int companyID);
        bool CheckUnicityPositionById(string intitulePosition, int departementID, int id);
        Position GetPositionIncludingEmploiById(int id);
        bool CheckAssignedPositionsJob(int EmploiID);
        List<Position> GetAllExcludingSpecificPositions(List<int> PositionsIDList, int CompanyId);
    }
}
