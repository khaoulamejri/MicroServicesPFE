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
    public class AncienteServices : IAncienteServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AncienteServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public bool checkUnicity(Anciente anciente, bool create)
        {
            if (create)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                anciente.companyID = currentCompanyId;
                return !utOfWork.AncienteRepository.GetMany(d => d.ToAnc == anciente.ToAnc && d.PlanDroitCongeID == anciente.PlanDroitCongeID).Any();
            }
            else
                return !utOfWork.AncienteRepository.GetMany(d => d.ToAnc == anciente.ToAnc && d.Id != anciente.Id && d.companyID == anciente.companyID).Any();
        
    }

        public Anciente Create(Anciente Anciente)
        {
            try
            {
                utOfWork.AncienteRepository.Add(Anciente);
                utOfWork.Commit();
                return Anciente;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Delete(int AncienteId)
        {
            try
            {
                var Anciente = GetAncienteByID(AncienteId);
                if (Anciente != null)
                {
                    utOfWork.AncienteRepository.Delete(Anciente);
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

        public Anciente Edit(Anciente Anciente)
        {
            try
            {
                utOfWork.AncienteRepository.Update(Anciente);
                utOfWork.Commit();
                return Anciente;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Anciente GetAncienteByID(int id)
        {
            return utOfWork.AncienteRepository.Get(a => a.Id == id);
        }

        public List<Anciente> GetAncienteByPlanDroitCongeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.AncienteRepository.GetMany(d => d.companyID == currentCompanyId && d.PlanDroitCongeID == id).ToList();
        }

        public List<Anciente> GetAncienteByPlanDroitCongeIdcmp(int id, int CompanyId)
        {
            return utOfWork.AncienteRepository.GetMany(d => d.companyID == CompanyId && d.PlanDroitCongeID == id).ToList();
        }
    }
}
