using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class EmployeeServices : IEmployeeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public Employee GetEmployeeByID(int id)
        {
            return utOfWork.EmployeeRepository.GetAll().FirstOrDefault(d => d.Id == id);
        }

        public RemplacantModel GetRemplacantByID(int id)

        {
            var remplacants = utOfWork.EmployeeRepository.GetMany(o => o.Id != id)
                                                     .Select(o => new RemplacantModel
                                                     {
                                                         Id = o.Id,
                                                         UserCreatR = o.UserCreat,
                                                         UserModifR = o.UserCreat,
                                                         DateCreatR = o.DateCreat,
                                                         DateModifR = o.DateModif,
                                                         companyIDR = o.companyID,
                                                         NumeroPersonneR = o.NumeroPersonne,
                                                         NomR = o.Nom,
                                                         PrenomR = o.Prenom,
                                                         DateNaissanceR = o.DateNaissance,
                                                         CINR = o.CIN,
                                                        
                                                         DeliveryDateCinR = o.DeliveryDateCin,
                                                         PlaceCinR = o.PlaceCin,
                                                         PassportNumberR = o.PassportNumber,
                                                         ValidityDateRPR = o.ValidityDateRP,
                                                         RecruitementDateR = o.RecruitementDate,
                                                         TitularizationDateR = o.TitularizationDate,
                                                         TelR = o.Tel,
                                                         TelGSMR = o.TelGSM,
                                                         MailR = o.Mail,
                                                         LangueR = o.Langue,
                                                         AdresseR = o.Adresse,
                                                         VilleR = o.Ville,
                                                         CodePostalR = o.CodePostal,
                                                      
                                                         UserR = o.User,
                                                           PlanDroitCongeIDConsumed = o.PlanDroitCongeIDConsumed,

                                                          RegimeTravailID = o.RegimeTravailID,
                                                         ConsultantExterneR = o.ConsultantExterne
                                                     });
            return remplacants.Last();
        }
        public List<Employee> GetAllEmployeeRemplacantt(bool isSysAdmin, int id)
        {
            return utOfWork.EmployeeRepository.GetMany(e => isSysAdmin ? true : e.Id != id).ToList();
        }
        public List<Employee> GetAllEmployeeByCompanyId()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.EmployeeRepository.GetMany(t => t.companyID == currentCompanyId).ToList();
        }
        public List<RemplacantModel> GetAllEmployeeRemplacant(int id)
        {
            return utOfWork.EmployeeRepository.GetMany(o => o.Id != id)
                                                     .Select(o => new RemplacantModel
                                                     {
                                                         Id = o.Id,
                                                         UserCreatR = o.UserCreat,
                                                         UserModifR = o.UserCreat,
                                                         DateCreatR = o.DateCreat,
                                                         DateModifR = o.DateModif,
                                                         companyIDR = o.companyID,
                                                         NumeroPersonneR = o.NumeroPersonne,
                                                         NomR = o.Nom,
                                                         PrenomR = o.Prenom,
                                                         DateNaissanceR = o.DateNaissance,
                                                         CINR = o.CIN,
                                                   
                                                         DeliveryDateCinR = o.DeliveryDateCin,
                                                         PlaceCinR = o.PlaceCin,
                                                         PassportNumberR = o.PassportNumber,
                                                         ValidityDateRPR = o.ValidityDateRP,
                                                         RecruitementDateR = o.RecruitementDate,
                                                         TitularizationDateR = o.TitularizationDate,
                                                         TelR = o.Tel,
                                                         TelGSMR = o.TelGSM,
                                                         MailR = o.Mail,
                                                         LangueR = o.Langue,
                                                         AdresseR = o.Adresse,
                                                         VilleR = o.Ville,
                                                         CodePostalR = o.CodePostal,

                                                         UserR = o.User,
                                                         PlanDroitCongeIDConsumed = o.PlanDroitCongeIDConsumed,

                                                         RegimeTravailID = o.RegimeTravailID,
                                                         ConsultantExterneR = o.ConsultantExterne
                                                     }).ToList();
        }

        public List<Employee> GetAllEmployee()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.EmployeeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }
        public Employee Create(Employee Employee)
        {
            try
            {
                //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                // int currentCompanyId = int.Parse(session[2].Value);
                //   Employee.companyID = currentCompanyId;

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
        public AffectationEmployee GetAffectationActif(int EmployeeId)
        {
            var listAffectationEmployee = GetAffectationEmployeeByEmployeeId(EmployeeId);
            if (listAffectationEmployee != null)
            {
                int AffectId = 0;
                foreach (var affect in listAffectationEmployee)
                {
                    if ((affect.DateDebut <= DateTime.Now) && (affect.DateFin >= DateTime.Now))
                    {
                        AffectId = affect.Id;
                    }
                }
                if (AffectId != 0)
                {
                    return GetAffectationEmployeeByID(AffectId);
                }
                return null;
            }
            else
                return null;
        }
        public  List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.companyID == currentCompanyId && d.EmployeeID == id).ToList();
        }
        public AffectationEmployee GetAffectationEmployeeByID(int id)
        {
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.Id == id).First();
        }

        public Employee Delete(int EmployeeId)
        {
            throw new NotImplementedException();
        }
    }
}
