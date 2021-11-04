using Microsoft.AspNetCore.Http;
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
    public class EmployeeVehiculeServices : IEmployeeVehiculeServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public EmployeeVehiculeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }

        public EmployeeVehicule GetEmployeeVehiculeByID(int id)
        {
            return utOfWork.EmployeeVehiculeRepository.Get(a => a.Id == id);
        }

        public List<EmployeeVehicule> GetAllEmployeeVehicule()
        {
            return utOfWork.EmployeeVehiculeRepository.GetAll().ToList();
        }

        public EmployeeVehicule Create(EmployeeVehicule employeeVehicule)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                employeeVehicule.companyID = currentCompanyId;
                utOfWork.EmployeeVehiculeRepository.Add(employeeVehicule);
                utOfWork.Commit();
                return employeeVehicule;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public EmployeeVehicule Edit(EmployeeVehicule employeeVehicule)
        {
            try
            {
                utOfWork.EmployeeVehiculeRepository.Update(employeeVehicule);
                utOfWork.Commit();
                return employeeVehicule;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public EmployeeVehicule Delete(int employeeVehiculeId)
        {
            try
            {
                var employeeVehicule = GetEmployeeVehiculeByID(employeeVehiculeId);
                if (employeeVehicule != null)
                {
                    utOfWork.EmployeeVehiculeRepository.Delete(employeeVehicule);
                    utOfWork.Commit();
                    return employeeVehicule;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<EmployeeVehicule> GetAllEmployeeVehiculeByEmployeeId(int id)
        {
            return utOfWork.EmployeeVehiculeRepository.GetMany(v => v.EmployeeId == id).ToList();
        }

        public List<EmployeeVehicule> GetAllEmployeeVehiculeByEmployeeIdAndDate(int id, DateTime dateDebutNote, DateTime dateFinNote)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.EmployeeVehiculeRepository.GetMany(v => v.EmployeeId == id && v.companyID == currentCompanyId
            && dateDebutNote.Date >= v.DateDebut.Date && dateDebutNote.Date <= v.DateFin.Date).ToList();
        }

        public List<Boolean> GetAllTypeVehiculeByEmployeeId(int id)
        {
            var listEmpVeh = GetAllEmployeeVehiculeByEmployeeId(id);
            var result = listEmpVeh.Select(m => m.TiTulaireVehProf).Distinct().ToList();
            return result;
        }

        public bool checkUnicity(EmployeeVehicule employeeVehicule)
        {
            return !utOfWork.EmployeeVehiculeRepository.GetMany(emV => emV.Matricule == employeeVehicule.Matricule && emV.Id != employeeVehicule.Id).Any();
        }
    }
}
