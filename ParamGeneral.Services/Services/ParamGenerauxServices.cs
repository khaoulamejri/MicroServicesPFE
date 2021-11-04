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
    public class ParamGenerauxServices : IParamGenerauxServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParamGenerauxServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public ParamGeneraux GetParamGeneraux()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var lstParamGeneraux = utOfWork.ParamGenerauxRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
            if (lstParamGeneraux.Any())
            {
                return lstParamGeneraux[0];
            }
            else
            {
                var parmGeneraux = new ParamGeneraux();
                return parmGeneraux;
            }
        }

        public ParamGeneraux Create(ParamGeneraux paramGeneraux)
        {
            try
            {
                utOfWork.ParamGenerauxRepository.Add(paramGeneraux);
                utOfWork.Commit();
                return paramGeneraux;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ParamGeneraux Edit(ParamGeneraux paramGeneraux)
        {
            try
            {
                if (utOfWork.ParamGenerauxRepository.GetMany(d => d.Id == paramGeneraux.Id).Any())
                {
                    utOfWork.ParamGenerauxRepository.Update(paramGeneraux);
                    utOfWork.Commit();
                    return paramGeneraux;
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

        public ParamGeneraux GetParamGenerauxByID(int id)
        {
            return utOfWork.ParamGenerauxRepository.Get(a => a.Id == id);
        }

        public bool CheckUnicityParamGeneraux(int id)
        {
            return !utOfWork.ParamGenerauxRepository.GetMany(d => d.Id == id).Any();
        }
    }
}