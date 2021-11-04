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
    public class TypeOrdreMissionServices : ITypeOrdreMissionServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public TypeOrdreMissionServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public bool checkUnicity(TypeOrdreMission typeOrdreMission)
        {
            return !utOfWork.TypeOrdreMissionRepository.GetMany(d => d.Intitule.ToLower() == typeOrdreMission.Intitule.ToLower() && d.companyID == typeOrdreMission.companyID && d.Id != typeOrdreMission.Id).Any();
        }

        public TypeOrdreMission Create(TypeOrdreMission typeOrdreMission)
        {
            try
            {
                utOfWork.TypeOrdreMissionRepository.Add(typeOrdreMission);
                utOfWork.Commit();
                return typeOrdreMission;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TypeOrdreMission Delete(TypeOrdreMission typeOrdreMission)
        {
            try
            {
                utOfWork.TypeOrdreMissionRepository.Delete(typeOrdreMission);
                utOfWork.Commit();
                return typeOrdreMission;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TypeOrdreMission Edit(TypeOrdreMission typeOrdreMission)
        {
            try
            {
                utOfWork.TypeOrdreMissionRepository.Update(typeOrdreMission);
                utOfWork.Commit();
                return typeOrdreMission;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TypeOrdreMission> GetAllTypeOrdreMission()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.TypeOrdreMissionRepository.GetMany(tom => tom.companyID == currentCompanyId).ToList();
        }

        public void Dispose()
        {
            utOfWork.Dispose();
        }

        public TypeOrdreMission GetTypeOrdreMissionById(int id)
        {
            return utOfWork.TypeOrdreMissionRepository.Get(a => a.Id == id);
        }
    }
}
