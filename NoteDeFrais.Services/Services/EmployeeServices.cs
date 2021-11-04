using Microsoft.AspNetCore.Http;
using NoteDeFrais.Data;
using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Domain.Enum;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.Services
{
    public class EmployeeServices : IEmployeeServices

    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
     //   private readonly IWFDocumentRepository WFDocumentRepository;

        //   private readonly IAffectationEmployeeServices affectationEmployeeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public Employee Create(Employee Employee)
        {
            try
            {
                //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                //int currentCompanyId = int.Parse(session[2].Value);
                //Employee.companyID = currentCompanyId;

                utOfWork.EmployeeRepository.Add(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

     

        public Employee Edit(Employee Employee)
        {
            try
            {
                
                utOfWork.EmployeeRepository.Update(Employee);
                utOfWork.Commit();
                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return utOfWork.EmployeeRepository.GetAll().ToList();
        }

        public Employee GetEmployeeByID(int id)
        {
            return utOfWork.EmployeeRepository.GetAll().FirstOrDefault(d => d.Id == id);

        }

        public Employee Delete(int EmployeeId)
        {
            throw new NotImplementedException();
        }
        public List<WFDocument> GetDocumentsToBeValidatedByEmployee(int employeeId, string documentType)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int companyId = int.Parse(session[2].Value);

            var wfDocToBeValidated = new List<WFDocument>();
            wfDocToBeValidated = utOfWork.WFDocumentRepository.GetMany(wf => wf.Finished == false && wf.AffectedToId == employeeId && wf.TypeDocument == documentType && wf.companyID == companyId).ToList();
            return wfDocToBeValidated;
        }

        public string checkMiisonOrdersAndLeaveDates(int employeeId, DateTime dateDebutOrdre, DateTime dateFinOrdre)
        {
            List<DemandeConge> validatedLeaves = getValidatedLeavesByEmployeeId(employeeId);
            if (validatedLeaves.Any())
            {
                foreach (DemandeConge leave in validatedLeaves)
                {
                    if ((dateDebutOrdre >= leave.DateDebutConge.Date && dateDebutOrdre < leave.DateRepriseConge.Date) ||
                        (dateFinOrdre >= leave.DateDebutConge.Date && dateFinOrdre <= leave.DateRepriseConge.Date) ||
                        (dateDebutOrdre <= leave.DateDebutConge.Date && dateFinOrdre >= leave.DateRepriseConge.Date))
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                        var dates = leave.DateDebutConge.GetDateTimeFormats(culture)[0] + " " + leave.DateRepriseConge.GetDateTimeFormats(culture)[0];
                        return dates;
                    }
                }
            }
            return "";
        }

        public List<DemandeConge> getValidatedLeavesByEmployeeId(int employeeId)
        {
            List<DemandeConge> DemandeCongeList = (from o in Context.DemandeConge
                                                   where (o.Statut == StatusDocument.valider && o.EmployeeIDConsumed == employeeId)
                                                   select new DemandeConge
                                                   {
                                                       Id = o.Id,
                                                       DateDebutConge = o.DateDebutConge,
                                                       DateRepriseConge = o.DateRepriseConge
                                                   }).ToList();
            return DemandeCongeList;
        }

        public Employee GetEmployeeByUserName(string user)
        {
            return utOfWork.EmployeeRepository.GetMany(d => d.User == user).FirstOrDefault();
        }

        public List<Employee> ListSoldeEmpLByhierarchyByCMPSelected(int companyId, string userName, bool SupInclut = true)
        {
            throw new NotImplementedException();
        }
        public Employee GetEmployeeByUserNameCompany()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var userName = session[0].Value;
            return utOfWork.EmployeeRepository.GetMany(d => d.User == userName && d.companyID == currentCompanyId).FirstOrDefault();
        }

    }
}
