using Microsoft.AspNetCore.Http;
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
    public class CompteComptableServices : ICompteComptableServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        public CompteComptableServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }

        public bool checkUnicity(CompteComptable compteComptable)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            compteComptable.companyID = currentCompanyId;
            if (compteComptable.Id != 0)
                return !utOfWork.CompteComptableRepository.GetMany(d => d.Compte == compteComptable.Compte && d.companyID == currentCompanyId && d.Id != compteComptable.Id).Any();
            else
                return !utOfWork.CompteComptableRepository.GetMany(d => d.Compte == compteComptable.Compte && d.companyID == currentCompanyId).Any();
        }

        public CompteComptable Create(CompteComptable compteComptable)
        {
            try
            {
                utOfWork.CompteComptableRepository.Add(compteComptable);
                utOfWork.Commit();
                return compteComptable;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public CompteComptable Delete(int compteComptableId)
        {
            try
            {
                var compteComptable = GetCompteComptableByID(compteComptableId);
                if (compteComptable != null)
                {
                    utOfWork.CompteComptableRepository.Delete(compteComptable);
                    utOfWork.Commit();
                    return compteComptable;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public CompteComptable Edit(CompteComptable compteComptable)
        {
            try
            {
                utOfWork.CompteComptableRepository.Update(compteComptable);
                utOfWork.Commit();
                return compteComptable;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<CompteComptable> GetAllCompteComptableByCompanyID()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.CompteComptableRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public CompteComptable GetCompteComptableByID(int id)
        {
            return utOfWork.CompteComptableRepository.Get(a => a.Id == id);
        }
    }
}
