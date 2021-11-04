using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class DepensesServices : IDepensesServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public DepensesServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }

        public Depenses GetDepensesByID(int id)
        {
            return utOfWork.DepensesRepository.Get(a => a.Id == id);
        }
        public List<Depenses> GetAllDepensesIncludedByNoteFrais(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var list = utOfWork.DepensesRepository.GetMany(d => d.companyID == currentCompanyId && d.NotesFraisID == id).Include(dd => dd.TypeDepense).Include(dd => dd.MoyenPaiement).ToList();
            var depences = new List<Depenses>();
            foreach (var item in list)
            {
                depences.Add(item);
            }
            return depences;
        }


        public List<Depenses> GetAllDepenses()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.DepensesRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }


        public Depenses Create(Depenses depenses)
        {
            try
            {
                utOfWork.DepensesRepository.Add(depenses);
                utOfWork.Commit();
                return depenses;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Depenses Edit(Depenses depenses)
        {
            try
            {
                utOfWork.DepensesRepository.Update(depenses);
                utOfWork.Commit();
                return depenses;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Depenses Delete(int depensesId)
        {
            try
            {
                var depenses = GetDepensesByID(depensesId);
                if (depenses != null)
                {
                    utOfWork.DepensesRepository.Delete(depenses);
                    utOfWork.Commit();
                    return depenses;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkDateInNotePeriod(Depenses depense, DateTime dateDebutNote, DateTime dateFinNote)
        {
            if (depense.DateDepense.Date >= dateDebutNote.Date && depense.DateDepense.Date <= dateFinNote.Date)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                depense.companyID = currentCompanyId;
                return true;
            }
            else return false;
        }

        public List<Depenses> GetAllDepensesByNoteFraisId(int noteFraisId)
        {
            return utOfWork.DepensesRepository.GetMany(dep => dep.NotesFraisID == noteFraisId).ToList();
        }
    }
}
