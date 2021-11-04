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
    public class JoursFeriesServices: IJoursFeriesServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JoursFeriesServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<JoursFeries> GetAllJoursFeries()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.JoursFeriesRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public JoursFeries Create(JoursFeries joursFeries)
        {
            try
            {
                utOfWork.JoursFeriesRepository.Add(joursFeries);
                utOfWork.Commit();
                return joursFeries;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public JoursFeries GetJoursFeriesByID(int id)
        {
            return utOfWork.JoursFeriesRepository.Get(a => a.Id == id);
        }

        public JoursFeries Edit(JoursFeries joursFeries)
        {
            try
            {
                utOfWork.JoursFeriesRepository.Update(joursFeries);
                utOfWork.Commit();
                return joursFeries;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public JoursFeries Delete(int JoursFeriesId)
        {

            try
            {
                JoursFeries joursFeries = GetJoursFeriesByID(JoursFeriesId);
                if (joursFeries != null)
                {
                    utOfWork.JoursFeriesRepository.Delete(joursFeries);
                    utOfWork.Commit();
                    return joursFeries;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool CheckUnicity(JoursFeries joursFeries, int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            joursFeries.companyID = currentCompanyId;
            return !utOfWork.JoursFeriesRepository.GetMany(d => d.jour == joursFeries.jour && d.companyID == joursFeries.companyID && d.Id != id).Any();
        }

        public bool CheckUnicity(JoursFeries joursFeries)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            joursFeries.companyID = currentCompanyId;
            return !utOfWork.JoursFeriesRepository.GetMany(d => d.jour == joursFeries.jour && d.companyID == joursFeries.companyID && d.Id != joursFeries.Id).Any();
        }
    }
}
