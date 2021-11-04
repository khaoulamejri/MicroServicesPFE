


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParamGeneral.Data;
using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Services
{
    public class AffectationEmployeeServices : IAffectationEmployeeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
     //   private readonly IPositionServices positionServices;
        private readonly IEmployeeServices employeeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AffectationEmployeeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
        
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public AffectationEmployee Create(AffectationEmployee AffectationEmployee)
        {
            try
            {
                if (!utOfWork.AffectationEmployeeRepository.GetMany(d => d.PositionID == AffectationEmployee.PositionID).Any())
                {
                    utOfWork.AffectationEmployeeRepository.Add(AffectationEmployee);
                    utOfWork.Commit();
                    return AffectationEmployee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Position> GetAllVacantPositionByIntervalleDate(DateTime Startdate, DateTime EndDate)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var postOccup = new List<string>();

            var postesOccuppees = utOfWork.AffectationEmployeeRepository.GetMany(t => t.DateDebut >= Startdate && t.DateFin > EndDate && t.companyID == currentCompanyId ||
            (t.DateDebut <= Startdate && t.DateFin < EndDate && Startdate < t.DateFin && t.companyID == currentCompanyId) ||
            (t.DateDebut >= Startdate && t.DateFin <= EndDate && t.companyID == currentCompanyId) ||
            (t.DateDebut <= Startdate && t.DateFin >= EndDate && t.companyID == currentCompanyId)).Include(e => e.Position).ToList();
            if (postesOccuppees != null)
            {
                foreach (var pos in postesOccuppees)
                {
                    postOccup.Add(pos.Position.Code);
                }
                return utOfWork.PositionRepository.GetMany(e => !postOccup.Contains(e.Code) && e.companyID == currentCompanyId).ToList();
            }
            else
                return null;
        }

        public List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.companyID == currentCompanyId && d.EmployeeID == id).Include(p => p.Position).ToList();
        }


        public List<AffectationEmployee> GetAffectationEmployeeByEmployeeIdByCMPSelected(int id)
        {
            var employee = utOfWork.EmployeeRepository.Get(a => a.Id == id);
            if (employee != null)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = employee.companyID;
                return utOfWork.AffectationEmployeeRepository.GetMany(d => d.companyID == currentCompanyId && d.EmployeeID == id).Include(p => p.Position).ToList();
            }
            else
                return null;
        }


        public List<AffectationEmployee> GetAffectationEmployeeByEmployeeIdWithoutCMP(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.EmployeeID == id).Include(p => p.Position).ToList();
        }

      
        public List<Position> SelectListItemPositions(DateTime dateDeb, DateTime dateFin)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return GetAllVacantPositionByIntervalleDate(dateDeb, dateFin).Where(t => t.companyID == currentCompanyId).ToList();
        }

        public AffectationEmployee GetAffectationEmployeeByID(int id)
        {
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.Id == id).Include(p => p.Position).First();
        }

        public List<AffectationEmployee> GetAffectationEmployeeByEmployeeIdcmp(int id)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //return utOfWork.AffectationEmployeeRepository.GetMany(d => d.companyID == currentCompanyId && d.EmployeeID == id).Include(p => p.Position).ToList();
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.EmployeeID == id).Include(p => p.Position).ToList();

        }

        public AffectationEmployee Edit(AffectationEmployee AffectationEmployee)
        {
            try
            {
                utOfWork.AffectationEmployeeRepository.Update(AffectationEmployee);
                utOfWork.Commit();
                return AffectationEmployee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Delete(int AffectationEmployeeId)
        {
            try
            {
                var AffectationEmployee = utOfWork.AffectationEmployeeRepository.Get(d => d.Id == AffectationEmployeeId);
                if (AffectationEmployee != null)
                {
                    utOfWork.AffectationEmployeeRepository.Delete(AffectationEmployee);
                    utOfWork.Commit();
                    return "ok";
                }
                else
                {
                    return "no";
                }
            }
            catch (Exception e)
            {
                return e.Message;
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

        public AffectationEmployee GetAffectationActifByCMPSelected(int EmployeeId)
        {
            var listAffectationEmployee = GetAffectationEmployeeByEmployeeIdByCMPSelected(EmployeeId);
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

        public AffectationEmployee GetAffectationActifWithoutCMP(int EmployeeId)
        {
            var listAffectationEmployee = GetAffectationEmployeeByEmployeeIdWithoutCMP(EmployeeId);
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

        public AffectationEmployee GetAffectationActifcmp(int EmployeeId)
        {
            var listAffectationEmployee = GetAffectationEmployeeByEmployeeIdcmp(EmployeeId);
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

        public List<AffectationEmployee> GetListAffectationEmployeeByPositionID(int id)
        {
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.PositionID == id).ToList();
        }

        public bool CheckUnicityPrincipalAffectationEmployee(int id, int employeeID, int companyID)
        {
            if (utOfWork.AffectationEmployeeRepository.GetMany(a => a.EmployeeID == employeeID && a.companyID == companyID && a.Principal == true && a.Id != id).Any())
                return true;
            else
                return false;
        }

        public bool CheckAffectationExistingTimeInterval(int positionID, DateTime dateDebut, DateTime dateFin, int employeeID)
        {
            return !utOfWork.AffectationEmployeeRepository.GetMany(d => d.PositionID != positionID && !((d.DateDebut <= dateDebut) || (d.DateFin >= dateFin)) && d.EmployeeID == employeeID).Any();
        }

        public List<SelectListItem> SelectListItemEmployee()
        {
            throw new NotImplementedException();
        }
    }
}