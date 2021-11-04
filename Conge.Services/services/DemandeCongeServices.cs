using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
 //public class DemandeCongeServices : IDemandeCongeServices
 //   {
 //       DatabaseFactory dbFactory = null;
 //       IUnitOfWork utOfWork = null;
 //       private readonly ApplicationDbContext Context;
 //       //  private readonly IHttpContextAccessor _httpContextAccessor;

 //       public DemandeCongeServices(ApplicationDbContext ctx)
 //       {
 //           Context = ctx;
 //           //  _httpContextAccessor = httpContextAccessor;
 //           dbFactory = new DatabaseFactory(ctx);
 //           utOfWork = new UnitOfWork(dbFactory);
 //       }

 //       public List<DemandeConge> GetAllDemandeConge()
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> GetDemandesCongeAutruiDelegation(int employeeId, string UserName)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge GetIncludedDemandeCongeByID(int id)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge GetDemandeCongeById(int id)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> GetDemandeCongeByEmployeeID(int id)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge Creat(DemandeConge DemandeConge)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge Delete(DemandeConge DemandeConge)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge Edit(DemandeConge DemandeConge)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public double GetAutomaticNumber(DateTime startDate, DateTime endDate, int EmployeeId, bool matinYNdeb, bool MatinYNfin)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public bool isWorkDay(DateTime Date, int CompanyID)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public int MonthDifference(DateTime lValue, DateTime rValue)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public double GetBonificationNumber(DateTime startDate, DateTime endDate, int EmployeeId)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DateTime GetDateReprise(DateTime endDate, double bonif, int CompanyID)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> GetDemandesCongeAutrui(int employeeId)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> getValidatedLeavesByEmployeeId(int employeeId)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public string checkMiisonOrdersAndLeaveDates(int employeeId, DateTime dateDebutOrdre, DateTime dateFinOrdre)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public bool isSubstituteBusy(DemandeConge DemandeConge, bool create)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public int GetDemandeCongeLimitPassed(TypeConge typeConge, DateTime DateDebutConge, int EmployeeID, float NbrConge, int demandeID)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public DemandeConge GetDemandeCongeByNumeroDemande(string numeroDemande, int companyId)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public bool CheckLeavesWithSameDates(DemandeConge demandeConge, bool create)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> GetAllDemandeCongeBetweenDatesByStatusDocument(DateTime datedebut, DateTime datereprise, StatusDocument? statut, int companyID)
 //       {
 //           throw new NotImplementedException();
 //       }

 //       public List<DemandeConge> GetAllDemandeCongeByCompanyID(int companyID)
 //       {
 //           throw new NotImplementedException();
 //       }
 //   }
}
