using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface ISoldeCongeServices
    {
        List<SoldeConge> GetAllSoldeConge();
        float GetAllSoldeCongeTotalByEmpId(int EmployeeId);
        List<SoldeConge> GetSoldeCongeByEmployeeId(int EmployeeId);
        List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId);
        float GetTotalSoldeCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId);
        List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId, int companyID);
        SoldeConge GetSoldeCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId, DateTime date, int companyID);
        SoldeConge Create(SoldeConge SoldeConge);
        SoldeConge Edit(SoldeConge SoldeConge);
        SoldeConge CreateOrUpdateFromMvtConge(MvtConge MvtConge);
        SoldeConge ValidateCongeFromMvtCongeFromOldSolde(MvtConge MvtConge);
        SoldeConge AnnulerCongeFromMvtCongeFromOldSolde(MvtConge MvtConge);

        List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeIdAllYear(int EmployeeId, int TypeCongeId, DateTime date);
    }
}
