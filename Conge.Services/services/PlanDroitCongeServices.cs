using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class PlanDroitCongeServices : IPlanDroitCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlanDroitCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public List<PlanDroitConge> GetAllPlanDroitConge()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.PlanDroitCongeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public PlanDroitConge GetPlanDroitCongeByID(int id)
        {
            //return utOfWork.PlanDroitCongeRepository.GetMany(d => d.Id == id).Include(a => a.Company).First();

            return utOfWork.PlanDroitCongeRepository.GetMany(d => d.Id == id).First();
        }

        public PlanDroitConge Create(PlanDroitConge PlanDroitConge)
        {
            try
            {
                utOfWork.PlanDroitCongeRepository.Add(PlanDroitConge);
                utOfWork.Commit();
                return PlanDroitConge;
            }
            catch
            {
                return null;
            }
        }

        public PlanDroitConge Edit(PlanDroitConge PlanDroitConge)
        {
            try
            {
                utOfWork.PlanDroitCongeRepository.Update(PlanDroitConge);
                utOfWork.Commit();
                return PlanDroitConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public PlanDroitConge Delete(int PlanDroitCongeId)
        {
            try
            {
                PlanDroitConge PlanDroitConge = GetPlanDroitCongeByID(PlanDroitCongeId);
                if (PlanDroitConge != null)
                {
                    utOfWork.PlanDroitCongeRepository.Delete(PlanDroitConge);
                    utOfWork.Commit();
                    return PlanDroitConge;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<SelectListItem> SelectListItemPlanDroitConge()
        {
            var PlanDroit = GetAllPlanDroitConge();
            List<SelectListItem> PlanDroitList = new List<SelectListItem>();
            foreach (var item in PlanDroit)
            {
                PlanDroitList.Add(new SelectListItem()
                {
                    Text = item.Display,
                    Value = item.Id.ToString()
                });
            }
            return PlanDroitList;
        }

        public PlanDroitConge GetPlanDroitCongeById(int id)
        {
            return utOfWork.PlanDroitCongeRepository.Get(a => a.Id == id);
        }

        public bool checkUnicity(PlanDroitConge PlanDroitConge)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            PlanDroitConge.companyID = currentCompanyId;
            return !utOfWork.PlanDroitCongeRepository.GetMany(d => d.Intitule == PlanDroitConge.Intitule && d.companyID == PlanDroitConge.companyID && d.Id != PlanDroitConge.Id).Any();
        }
    }
}
