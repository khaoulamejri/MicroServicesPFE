using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NoteDeFrais.Data;
using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.Services
{
    public class EmployeeGroupeServices : IEmployeeGroupeServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupeFraisServices _groupeFraisServices;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public EmployeeGroupeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, IGroupeFraisServices groupeFraisServices)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
            _groupeFraisServices = groupeFraisServices;
        }


        public EmployeeGroupe GetEmployeeGroupeByIdIncluded(int id)
        {
            return utOfWork.EmployeeGroupeRepository.GetMany(d => d.Id == id).Include(e => e.GroupeFrais).First();
        }

        public List<EmployeeGroupe> GetAllEmployeeGroupe()
        {
            return utOfWork.EmployeeGroupeRepository.GetAll().ToList();
        }


        public List<GroupeFrais> GetAllGroupeFraisByEmployee(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var listOfEmployeeGroupe = new List<EmployeeGroupe>();
            listOfEmployeeGroupe = utOfWork.EmployeeGroupeRepository.GetMany(e => e.EmployeeIDConsumed == id && e.companyID == currentCompanyId).Include(dd => dd.GroupeFrais).ToList();
            var listOfgroupeFrais = new List<GroupeFrais>();
            foreach (var l in listOfEmployeeGroupe)
            {
                listOfgroupeFrais.Add(l.GroupeFrais);
            }
            return listOfgroupeFrais;
        }


        public EmployeeGroupe Create(EmployeeGroupe employeeGroupe)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                employeeGroupe.companyID = currentCompanyId;
                utOfWork.EmployeeGroupeRepository.Add(employeeGroupe);
                utOfWork.Commit();
                return employeeGroupe;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public EmployeeGroupe Edit(EmployeeGroupe employeeGroupe)
        {
            try
            {
                utOfWork.EmployeeGroupeRepository.Update(employeeGroupe);
                utOfWork.Commit();
                return employeeGroupe;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public EmployeeGroupe Delete(int employeeGroupeId)
        {
            try
            {
                var employeeGroupe = utOfWork.EmployeeGroupeRepository.Get(a => a.Id == employeeGroupeId);
                if (employeeGroupe != null)
                {
                    utOfWork.EmployeeGroupeRepository.Delete(employeeGroupe);
                    utOfWork.Commit();
                    return employeeGroupe;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<EmployeeGroupe> GetAllEmployeeGroupeByEmployeeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.EmployeeGroupeRepository.GetMany(v => v.EmployeeIDConsumed == id).Include(e => e.GroupeFrais).ToList();
        }

        public GroupeFrais GetGroupeByEmployeeIdDateNote(int employeeId, DateTime startDate, DateTime endDate)
        {
            var eg = utOfWork.EmployeeGroupeRepository.GetMany(v => v.EmployeeIDConsumed == employeeId && v.DateAffectation <= startDate && v.DateFinAffectation >= endDate).Include(e => e.GroupeFrais).FirstOrDefault();
            if (eg != null)
            {
                return _groupeFraisServices.GetGroupeFraisByID(eg.GroupeFraisID);
            }
            return null;
        }

        public List<EmployeeGroupe> GetGroupeByEmployeeIdIncludeGroupeFraisDepense(int employeeId)
        {
            return utOfWork.EmployeeGroupeRepository.GetMany(eg => eg.EmployeeIDConsumed == employeeId).Include(g => g.GroupeFrais.GroupeFraisDepense).ToList();
        }

        public EmployeeGroupe GetEmployeeGroupeById(int id)
        {
            return utOfWork.EmployeeGroupeRepository.Get(a => a.Id == id);
        }


        // à optimiser
        public bool checkEmployeeGroup(EmployeeGroupe employeeGroupe)
        {
            if (utOfWork.EmployeeGroupeRepository.GetMany(a => a.EmployeeIDConsumed == employeeGroupe.EmployeeIDConsumed && a.DateFinAffectation >= employeeGroupe.DateFinAffectation && a.DateAffectation <= employeeGroupe.DateFinAffectation && a.GroupeFraisID == employeeGroupe.GroupeFraisID && a.Id != employeeGroupe.Id).Any()
                || utOfWork.EmployeeGroupeRepository.GetMany(a => a.EmployeeIDConsumed == employeeGroupe.EmployeeIDConsumed && a.DateAffectation >= employeeGroupe.DateAffectation && a.DateFinAffectation <= employeeGroupe.DateFinAffectation && a.GroupeFraisID == employeeGroupe.GroupeFraisID && a.Id != employeeGroupe.Id).Any()
                || utOfWork.EmployeeGroupeRepository.GetMany(a => a.EmployeeIDConsumed == employeeGroupe.EmployeeIDConsumed && a.DateFinAffectation >= employeeGroupe.DateAffectation && a.DateAffectation <= employeeGroupe.DateAffectation && a.GroupeFraisID == employeeGroupe.GroupeFraisID && a.Id != employeeGroupe.Id).Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
