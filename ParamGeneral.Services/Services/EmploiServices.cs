using Microsoft.AspNetCore.Http;
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
    public class EmploiServices : IEmploiServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmploiServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Emploi> GetAllEmploi()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.EmploiRepository.GetMany(e => e.companyID == currentCompanyId).ToList();
        }
        public Emploi Create(Emploi Emploi)
        {
            try
            {
                utOfWork.EmploiRepository.Add(Emploi);
                utOfWork.Commit();
                return Emploi;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Emploi Delete(int EmploiId)
        {
            try
            {
                var Emploi = utOfWork.EmploiRepository.Get(a => a.Id == EmploiId);
                if (Emploi != null)
                {
                    utOfWork.EmploiRepository.Delete(Emploi);
                    utOfWork.Commit();
                    return Emploi;
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

        public Emploi Edit(Emploi Emploi)
        {
            try
            {
                utOfWork.EmploiRepository.Update(Emploi);
                utOfWork.Commit();
                return Emploi;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Emploi> GetAllEmploiByCompany(int CompanyId)
        {
            return utOfWork.EmploiRepository.GetMany(e => e.companyID == CompanyId).ToList();
        }

        public Emploi GetEmploiById(int EmploiId)
        {
            return utOfWork.EmploiRepository.GetMany(e => e.Id == EmploiId).Include(e => e.Positions).FirstOrDefault();
        }

        public List<Emploi> GetAllEmploiByKey(string key)
        {
            //  var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //  return utOfWork.EmploiRepository.GetMany(e => (e.Code.Equals(key) || e.Intitule.Equals(key) || e.Reference.Equals(key) || e.Description.Equals(key)) && e.companyID == currentCompanyId).ToList();
            return utOfWork.EmploiRepository.GetMany(e => (e.Code.Equals(key) || e.Intitule.Equals(key) || e.Reference.Equals(key) || e.Description.Equals(key))).ToList();


            ////////utOfWork.EmploiRepository.GetAll().ToList();
        }
        public bool checkUnicity(Emploi Emploi, bool create)
        {
            if (create)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                return !utOfWork.EmploiRepository.GetMany(d => d.Reference.ToLower() == Emploi.Reference.ToLower()).Any();

            }
            else
            {
                return !utOfWork.EmploiRepository.GetMany(d => d.Id != Emploi.Id && d.Reference.ToLower() == Emploi.Reference.ToLower()).Any();
            }
        }
    }
}