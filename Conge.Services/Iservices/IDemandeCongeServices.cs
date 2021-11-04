using Conge.Domain.Entities;
using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IDemandeCongeServices
    {
        List<DemandeConge> GetAllDemandeConge();
        List<DemandeConge> GetDemandesCongeAutruiDelegation(int employeeId, string UserName);
        DemandeConge GetIncludedDemandeCongeByID(int id);
        DemandeConge GetDemandeCongeById(int id);
        List<DemandeConge> GetDemandeCongeByEmployeeID(int id);
        DemandeConge Creat(DemandeConge DemandeConge);
        DemandeConge Delete(DemandeConge DemandeConge);
        DemandeConge Edit(DemandeConge DemandeConge);
        double GetAutomaticNumber(DateTime startDate, DateTime endDate, int EmployeeId, Boolean matinYNdeb, Boolean MatinYNfin);
        bool isWorkDay(DateTime Date, int CompanyID);
        int MonthDifference(DateTime lValue, DateTime rValue);
        double GetBonificationNumber(DateTime startDate, DateTime endDate, int EmployeeId);
        DateTime GetDateReprise(DateTime endDate, double bonif, int CompanyID);
        List<DemandeConge> GetDemandesCongeAutrui(int employeeId);
        List<DemandeConge> getValidatedLeavesByEmployeeId(int employeeId);
        string checkMiisonOrdersAndLeaveDates(int employeeId, DateTime dateDebutOrdre, DateTime dateFinOrdre);
        bool isSubstituteBusy(DemandeConge DemandeConge, bool create);
        int GetDemandeCongeLimitPassed(TypeConge typeConge, DateTime DateDebutConge, int EmployeeID, float NbrConge, int demandeID);
        DemandeConge GetDemandeCongeByNumeroDemande(string numeroDemande, int companyId);
        bool CheckLeavesWithSameDates(DemandeConge demandeConge, bool create);
        List<DemandeConge> GetAllDemandeCongeBetweenDatesByStatusDocument(DateTime datedebut, DateTime datereprise, StatusDocument? statut, int companyID);
        List<DemandeConge> GetAllDemandeCongeByCompanyID(int companyID);
    }
}
