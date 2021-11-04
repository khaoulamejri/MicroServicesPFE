using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class AffectationEmployeeServices : IAffectationEmployeeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext Context;

        public AffectationEmployeeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;

            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
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
            public  AffectationEmployee GetAffectationEmployeeByID(int id)
        {
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.Id == id).First();
        }

        public List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.AffectationEmployeeRepository.GetMany(d => d.companyID == currentCompanyId && d.EmployeeID == id).ToList();
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

    }
}
