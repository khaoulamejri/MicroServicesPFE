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
    public class FraisKilometriquesServices : IFraisKilometriquesServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public FraisKilometriquesServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }


        public FraisKilometriques GetFraisKilometriquesByID(int id)
        {
            return utOfWork.FraisKilometriquesRepository.Get(a => a.Id == id);
        }


        public List<FraisKilometriques> GetAllFraisKilometriquesByNoteFrais(int notesFraisId)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.FraisKilometriquesRepository.GetMany(d => d.companyID == currentCompanyId && d.NotesFrais.Id == notesFraisId).ToList();
        }



        public List<FraisKilometriques> GetAllFraisKilometriques()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.FraisKilometriquesRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }


        public FraisKilometriques Create(FraisKilometriques fraisKilometriques)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                fraisKilometriques.companyID = currentCompanyId;
                utOfWork.FraisKilometriquesRepository.Add(fraisKilometriques);
                utOfWork.Commit();
                return fraisKilometriques;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public FraisKilometriques Edit(FraisKilometriques fraisKilometriques)
        {
            try
            {
                utOfWork.FraisKilometriquesRepository.Update(fraisKilometriques);
                utOfWork.Commit();
                return fraisKilometriques;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public FraisKilometriques Delete(int fraisKilometriquesId)
        {
            try
            {
                var fraisKilometriques = GetFraisKilometriquesByID(fraisKilometriquesId);
                if (fraisKilometriques != null)
                {
                    utOfWork.FraisKilometriquesRepository.Delete(fraisKilometriques);
                    utOfWork.Commit();
                    return fraisKilometriques;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
