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
    public class MoyenPaiementServices : IMoyenPaiementServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public MoyenPaiementServices(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public MoyenPaiement GetMoyenPaiementByID(int id)
        {
            return utOfWork.MoyenPaiementRepository.Get(a => a.Id == id);
        }

        public List<MoyenPaiement> GetAllMoyenPaiement()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.MoyenPaiementRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }


        public MoyenPaiement Create(MoyenPaiement moyenPaiement)
        {
            try
            {
                utOfWork.MoyenPaiementRepository.Add(moyenPaiement);
                utOfWork.Commit();
                return moyenPaiement;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public MoyenPaiement Edit(MoyenPaiement moyenPaiement)
        {
            try
            {
                utOfWork.MoyenPaiementRepository.Update(moyenPaiement);
                utOfWork.Commit();
                return moyenPaiement;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public MoyenPaiement Delete(int moyenPaiementId)
        {
            try
            {
                var moyenPaiement = GetMoyenPaiementByID(moyenPaiementId);
                if (moyenPaiement != null)
                {
                    utOfWork.MoyenPaiementRepository.Delete(moyenPaiement);
                    utOfWork.Commit();
                    return moyenPaiement;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkUnicity(MoyenPaiement moyenPaiement, bool create)
        {
            if (create)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                moyenPaiement.companyID = currentCompanyId;
            }
            return !utOfWork.MoyenPaiementRepository.GetMany(d => d.Id != moyenPaiement.Id && (d.Code == moyenPaiement.Code || d.Intitule.ToLower() == moyenPaiement.Intitule.ToLower()) && d.companyID == moyenPaiement.companyID).Any();
        }
    }
}
