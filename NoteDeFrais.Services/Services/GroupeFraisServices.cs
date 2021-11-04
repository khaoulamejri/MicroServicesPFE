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
    public class GroupeFraisServices : IGroupeFraisServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public GroupeFraisServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }
        public GroupeFrais GetGroupeFraisByID(int id)
        {
            return utOfWork.GroupeFraisRepository.Get(a => a.Id == id);
        }

        public List<GroupeFrais> GetAllGroupeFrais()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.GroupeFraisRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public GroupeFrais Create(GroupeFrais groupeFrais)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                groupeFrais.companyID = currentCompanyId;
                utOfWork.GroupeFraisRepository.Add(groupeFrais);
                utOfWork.Commit();
                return groupeFrais;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public GroupeFrais Edit(GroupeFrais groupeFrais)
        {
            try
            {
                utOfWork.GroupeFraisRepository.Update(groupeFrais);
                utOfWork.Commit();
                return groupeFrais;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public GroupeFrais Delete(int groupeFraisId)
        {
            try
            {
                var groupeFrais = GetGroupeFraisByID(groupeFraisId);
                if (groupeFrais != null)
                {
                    utOfWork.GroupeFraisRepository.Delete(groupeFrais);
                    utOfWork.Commit();
                    return groupeFrais;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkUnicity(GroupeFrais groupeFrais)
        {
            return !utOfWork.GroupeFraisRepository.GetMany(d => (d.Intitule.ToLower() == groupeFrais.Intitule.ToLower()) && d.Id != groupeFrais.Id).Any();
        }
    }
}
