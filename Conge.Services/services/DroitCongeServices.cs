using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class DroitCongeServices : IDroitCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DroitCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<DroitConge> GetAllDroitConge()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            // return utOfWork.DroitCongeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
            return utOfWork.DroitCongeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();

           // return utOfWork.DroitCongeRepository.GetMany(d => d.companyID == currentCompanyId).Include(d => d.Details_DroitConge).ToList();
        }

        public DroitConge Create(DroitConge droitConge)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                int numero = 0;
                numero = utOfWork.DroitCongeRepository.GetMany(t => t.companyID == currentCompanyId).AsEnumerable().Select(p => Convert.ToInt32(p.Numero)).DefaultIfEmpty(0).Max();
                numero++;
                droitConge.Numero = numero.ToString();
                utOfWork.DroitCongeRepository.Add(droitConge);
                utOfWork.Commit();
                return droitConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DroitConge GetDroitCongeByID(int id)
        {
            return utOfWork.DroitCongeRepository.Get(a => a.Id == id);
        }

        public List<DroitConge> GetDroitCongeByDate(DateTime date)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);

            return utOfWork.DroitCongeRepository.GetMany(d => d.MoisAffectation.Month == date.Month && d.MoisAffectation.Year == date.Year && d.companyID == currentCompanyId).Include(d => d.Details_DroitConge).ToList();
        }

        public DroitConge Edit(DroitConge DroitConge)
        {
            try
            {
                utOfWork.DroitCongeRepository.Update(DroitConge);
                utOfWork.Commit();
                return DroitConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
