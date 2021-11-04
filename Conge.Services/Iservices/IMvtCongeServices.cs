using Conge.Domain.Entities;
using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IMvtCongeServices
    {
        List<MvtConge> GetMvtCongeByEmployeeId(int EmployeeId);
        List<MvtConge> GetMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId);
        MvtConge GetLastMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId);
        List<MvtConge> GetLastMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId, bool Sens = false);
        List<MvtConge> GetMvtCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId, DateTime date, bool Sens = false);
        MvtConge Create(MvtConge MvtConge);
        MvtConge Edit(MvtConge MvtConge);
        MvtConge ValidateMvtSoldeConge(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens);
        MvtConge ValidateMvtSoldeCongeFromDemande(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens);
        MvtConge AnnulerMvtSoldeCongeFromDemande(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens);
        bool ExistMvtCongeInDate(int TypeCongeID, DateTime date, int companyID, RhSens sens);
        List<MvtConge> GetAllMvtConge();


    }
}
