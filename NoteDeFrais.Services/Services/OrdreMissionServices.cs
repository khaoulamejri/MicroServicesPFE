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
    public class OrdreMissionServices : IOrdreMissionServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        private readonly IEmployeeServices employeeServices;
        IUnitOfWork utOfWork = null;

        public OrdreMissionServices(
            IHttpContextAccessor httpContextAccessor, IEmployeeServices emp,
            ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
            employeeServices = emp;
        }

        public OrdreMission Create(OrdreMission ordreMission)
        {
            try
            {
                utOfWork.OrdreMissionRepository.Add(ordreMission);
                utOfWork.Commit();
                return ordreMission;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OrdreMission Delete(int OrdreMissionId)
        {
            try
            {
                var ordreMission = GetOrdreMissionById(OrdreMissionId);
                if (ordreMission != null)
                {
                    utOfWork.OrdreMissionRepository.Delete(ordreMission);
                    utOfWork.Commit();
                    return ordreMission;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OrdreMission Edit(OrdreMission ordreMission)
        {
            try
            {
                utOfWork.OrdreMissionRepository.Update(ordreMission);
                utOfWork.Commit();
                return ordreMission;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OrdreMission> GetAllOrdreMission()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.OrdreMissionRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

      
        public List<OrdreMissionVM> GetOrdreMissionByEmployeeId(int employeeId, int currentCompanyId)
        {
           var employes = employeeServices.GetAllEmployees();
            var empl = employes.Single(empl => empl.Id == employeeId);

            return utOfWork.OrdreMissionRepository.GetMany(o => o.companyID == currentCompanyId && o.EmployeeIDConsumed == employeeId)
                                                    .Select(o => new OrdreMissionVM
                                                    {
                                                        Id = o.Id,
                                                        Titre = o.Titre,
                                                        Description = o.Description,
                                                        EmployeeIDConsumed = o.EmployeeIDConsumed,
                                                        // Employee = o.Employee,
                                                        PaysIdConsumed = o.PaysIdConsumed,
                                                        TypeMissionOrderId = o.TypeMissionOrderId,
                                                        DateDebut = o.DateDebut,
                                                        DateFin = o.DateFin,
                                                        Statut = o.Statut,
                                                        NumeroOM = o.NumeroOM,
                                                        EmployeeName = empl.Nom + " " + empl.Prenom
                                                    }).ToList();
        }

        public OrdreMission GetOrdreMissionById(int id)
        {
            return utOfWork.OrdreMissionRepository.Get(a => a.Id == id);
        }

        public OrdreMission GetAllIncludedOrdreMissionById(int id)
        {
            return utOfWork.OrdreMissionRepository.GetMany(d => d.Id == id).FirstOrDefault();
        }

        public List<OrdreMission> GetOrdreMissionForOthers(string username, int employeeId, int companyId)
        {
            try
            {
                return ((employeeId > 0) ?
                    utOfWork.OrdreMissionRepository.GetMany(d => d.UserCreat == username && d.EmployeeIDConsumed != employeeId && d.companyID == companyId)
                    : utOfWork.OrdreMissionRepository.GetMany(d => d.UserCreat == username && d.companyID == companyId))
                    .ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkTitleUnicity(OrdreMission ordreMission)
        {
            return !utOfWork.OrdreMissionRepository.GetMany(d => d.Titre == ordreMission.Titre && d.companyID == ordreMission.companyID && d.Id != ordreMission.Id).Any();
        }

        public bool checkPeriodUnicity(OrdreMission ordreMission)
        {
            var validatedOrders = getValidatedMissionOrdersByEmployeeId(ordreMission.EmployeeIDConsumed).Where(x => x.Statut != StatusDocument.abondonner && x.Statut != StatusDocument.refuser && x.Statut != StatusDocument.annuler).ToList();
            if (validatedOrders.Any())
            {
                foreach (var order in validatedOrders)
                {
                    if (order.DateFin.Date >= ordreMission.DateDebut.Date && order.DateDebut.Date <= ordreMission.DateFin.Date)
                        return false;
                }

            }
            return true;
        }

        public List<OrdreMissionVM> getValidatedMissionOrdersByEmployeeId(int employeeId)
        {
            return utOfWork.OrdreMissionRepository.GetMany(o => o.Statut == StatusDocument.valider && o.EmployeeIDConsumed == employeeId)
                                                     .Select(o => new OrdreMissionVM
                                                     {
                                                         Id = o.Id,
                                                         Titre = o.Titre,
                                                         DateDebut = o.DateDebut,
                                                         DateFin = o.DateFin
                                                     }).ToList();
        }

        public List<OrdreMissionVM> getMissionOrdersByType(int missionOrderType)
        {
            return utOfWork.OrdreMissionRepository.GetMany(o => o.TypeMissionOrderId == missionOrderType)
                                                     .Select(o => new OrdreMissionVM
                                                     {
                                                         Id = o.Id,
                                                         Titre = o.Titre
                                                     }).ToList();
        }

        public bool checkNoteDates(NotesFrais notesFrais)
        {
            if (notesFrais.OrdreMissionId > 0)
            {
                var ordreMission = utOfWork.OrdreMissionRepository.Get(d => d.Id == notesFrais.OrdreMissionId);
                return (notesFrais.DateDebut.Date >= ordreMission.DateDebut.Date && notesFrais.DateDebut.Date <= ordreMission.DateFin.Date && notesFrais.DateFin.Date <= ordreMission.DateFin.Date);
            }
            else return true;
        }

        public string checkMiisonOrdersAndLeaveDates(int employeeId, System.DateTime dateDebutConge, System.DateTime dateRepriseConge)
        {
            var validatedOrders = getValidatedMissionOrdersByEmployeeId(employeeId);
            if (validatedOrders.Any())
            {
                foreach (var order in validatedOrders)
                {
                    if ((dateDebutConge >= order.DateDebut.Date && dateDebutConge <= order.DateFin.Date) ||
                        (dateRepriseConge > order.DateDebut.Date && dateRepriseConge <= order.DateFin.Date) ||
                        (dateDebutConge <= order.DateDebut.Date && dateRepriseConge >= order.DateFin.Date))
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                        return order.DateDebut.GetDateTimeFormats(culture)[0] + " " + order.DateFin.GetDateTimeFormats(culture)[0];
                    }
                }
            }
            return "";
        }

        public OrdreMissionVM ConvertToOrdreMissionVM(OrdreMission ordreMission)
        {
            var employes = employeeServices.GetAllEmployees();
            var empl = employes.Single(empl => empl.Id == ordreMission.EmployeeIDConsumed);
            return new OrdreMissionVM
            {
                Id = ordreMission.Id,
                Titre = ordreMission.Titre,
                Description = ordreMission.Description,
                Statut = ordreMission.Statut,
                EmployeeIDConsumed = ordreMission.EmployeeIDConsumed,
             // Employee = ordreMission.Employee,
                TypeMissionOrderId = ordreMission.TypeMissionOrderId,
                PaysIdConsumed = ordreMission.PaysIdConsumed,
                DateDebut = ordreMission.DateDebut,
                DateFin = ordreMission.DateFin,
                EmployeeName = empl.Nom + " " + empl.Prenom
            };
        }
    }
}
