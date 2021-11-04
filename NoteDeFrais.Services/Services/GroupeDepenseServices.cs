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
    public class GroupeDepenseServices : IGroupeDepenseServices
    {
        private readonly ITypeDepenseServices _typeDepenseServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public GroupeDepenseServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, ITypeDepenseServices typeDepenseServices)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
            _typeDepenseServices = typeDepenseServices;
        }
        public List<GroupeFraisDepense> GetAllGroupeFraisDepense()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.GroupeFraisDepenseRepository.GetMany(d => d.companyID == currentCompanyId).Include(x => x.GroupeFraisID).Include(p => p.TypeDepenseID).ToList();
        }

        public GroupeFraisDepense GetGroupeFraisDepenseByID(int id)
        {
            try
            {
                return utOfWork.GroupeFraisDepenseRepository.GetMany(d => d.Id == id).Include(y => y.TypeDepense).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GroupeFraisDepense GetGroupefraisByTypeDepense(int idTypeDepense, int GroupeFraisID)
        {
            try
            {
                return utOfWork.GroupeFraisDepenseRepository.GetMany(d => d.TypeDepenseID == idTypeDepense && d.GroupeFraisID == GroupeFraisID).Include(y => y.TypeDepense).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GroupeFraisDepense Create(GroupeFraisDepense groupeFraisDepense)
        {
            try
            {
                utOfWork.GroupeFraisDepenseRepository.Add(groupeFraisDepense);
                utOfWork.Commit();
                return groupeFraisDepense;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public GroupeFraisDepense Edit(GroupeFraisDepense groupeFraisDepense)
        {
            try
            {
                utOfWork.GroupeFraisDepenseRepository.Update(groupeFraisDepense);
                utOfWork.Commit();
                return groupeFraisDepense;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public GroupeFraisDepense Delete(int groupeFraisDepenseId)
        {
            try
            {
                var groupeFraisDepense = utOfWork.GroupeFraisDepenseRepository.Get(a => a.Id == groupeFraisDepenseId);
                if (groupeFraisDepense != null)
                {
                    utOfWork.GroupeFraisDepenseRepository.Delete(groupeFraisDepense);
                    utOfWork.Commit();
                    return groupeFraisDepense;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<TypeDepense> GetAllTypeDepenseByGroupeId(int id)
        {
            var groupList = utOfWork.GroupeFraisDepenseRepository.GetMany(g => g.GroupeFraisID == id).ToList();
            var tp = new List<TypeDepense>();
            if (groupList.Any())
            {
                foreach (var empl in groupList)
                {
                    tp.Add(_typeDepenseServices.GetTypeDepenseByIDIncluded(empl.TypeDepenseID));
                }
            }
            return tp;
        }

        public List<GroupeFraisDepense> GetAllGroupeDepenseByGroupeId(int id)
        {
            return utOfWork.GroupeFraisDepenseRepository.GetMany(g => g.GroupeFraisID == id).Include(x => x.TypeDepense).Include(x => x.GroupeFrais).ToList();
        }

        public bool checkUnicity(GroupeFraisDepense groupeFraisDepense)
        {
            return !utOfWork.GroupeFraisDepenseRepository.GetMany(d => d.TypeDepenseID == groupeFraisDepense.TypeDepenseID && d.GroupeFraisID == groupeFraisDepense.GroupeFraisID).Any();
        }
    }
}
