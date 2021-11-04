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
    public class TypeDepenseServices : ITypeDepenseServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public TypeDepenseServices(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }


        public TypeDepense GetTypeDepenseByIDIncluded(int id)
        {
            return utOfWork.TypeDepenseRepository.GetMany(d => d.Id == id).Include(d => d.CompteComptable).FirstOrDefault();
        }


        public List<TypeDepense> GetAllTypeDepense()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.TypeDepenseRepository.GetMany(tom => tom.companyID == currentCompanyId).Include(d => d.CompteComptable).ToList();
        }


        public TypeDepense Create(TypeDepense typeDepense)
        {
            try
            {
                utOfWork.TypeDepenseRepository.Add(typeDepense);
                utOfWork.Commit();
                return typeDepense;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public TypeDepense Edit(TypeDepense typeDepense)
        {
            try
            {
                utOfWork.TypeDepenseRepository.Update(typeDepense);
                utOfWork.Commit();
                return typeDepense;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public TypeDepense Delete(int TypeDepenseId)
        {
            try
            {
                var TypeDepense = GetTypeDepenseByIDIncluded(TypeDepenseId);
                if (TypeDepense != null)
                {
                    utOfWork.TypeDepenseRepository.Delete(TypeDepense);
                    utOfWork.Commit();
                    return TypeDepense;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkUnicity(TypeDepense typeDepense, bool create)
        {
            if (create)
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                typeDepense.companyID = currentCompanyId;
                return !utOfWork.TypeDepenseRepository.GetMany(d => d.companyID == typeDepense.companyID && d.Intitule.ToLower() == typeDepense.Intitule.ToLower()).Any();

            }
            else
            {
                return !utOfWork.TypeDepenseRepository.GetMany(d => d.companyID == typeDepense.companyID && d.Id != typeDepense.Id && d.Intitule.ToLower() == typeDepense.Intitule.ToLower()).Any();
            }
        }

        public TypeDepense GetTypeDepenseById(int id)
        {
            return utOfWork.TypeDepenseRepository.Get(a => a.Id == id);
        }
    }
}
