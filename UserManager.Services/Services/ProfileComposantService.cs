using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class ProfileComposantService : IProfileComposantService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileComposantService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public ProfileComposant Create(ProfileComposant profileComposant)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);

                if (!utOfWork.ProfileComposantRepository.GetMany(c => c.ProfileId == profileComposant.ProfileId && c.ComposantId == profileComposant.ComposantId && c.companyID == currentCompanyId).Any())
                {
                    var profile = utOfWork.ProfileRepository.GetMany(pr => pr.Id == profileComposant.ProfileId).FirstOrDefault();
                    var composant = utOfWork.ComposantRepository.GetMany(a => a.Id == profileComposant.ComposantId).FirstOrDefault();
                    if (composant != null && profile != null)
                        profileComposant.ProfileId = profile.Id;
                    profileComposant.ComposantId = composant.Id;
                    //profileComposant.Profile = profile;
                    //profileComposant.Composant = composant;
                    profileComposant.companyID = currentCompanyId;
                    utOfWork.ProfileComposantRepository.Add(profileComposant);
                    utOfWork.Commit();
                    return profileComposant;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string Delete(int ProfileId, int composantId)
        {
            try
            {
                var profileComposant = utOfWork.ProfileComposantRepository.Get(pc => pc.ProfileId == ProfileId && pc.ComposantId == composantId);
                if (profileComposant != null)
                {
                    utOfWork.ProfileComposantRepository.Delete(profileComposant);
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

        public ProfileComposant Edit(ProfileComposant ProfileComposant)
        {
            try
            {
                utOfWork.ProfileComposantRepository.Update(ProfileComposant);
                utOfWork.Commit();
                return ProfileComposant;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ProfileComposant> GetAllProfileComposants()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ProfileComposantRepository.GetMany(c => c.companyID == currentCompanyId).Include(x => x.Profile).Include(p => p.Composant).ToList();
        }

        public ProfileComposant GetProfileComposantById(int ProfileId, int composantId)
        {
            return utOfWork.ProfileComposantRepository.Get(pc => pc.ProfileId == ProfileId && pc.ComposantId == composantId);
        }

        public List<ProfileComposant> GetProfileComposantsByComposantId(int composantId)
        {
            return utOfWork.ProfileComposantRepository.GetMany(d => d.ComposantId == composantId).ToList();
        }

        public List<ProfileComposant> GetProfileComposantsByprofileId(int profileId)
        {
            return utOfWork.ProfileComposantRepository.GetMany(d => d.ProfileId == profileId).Include(pc => pc.Composant).Include(pc => pc.Profile).ToList();
        }
    }
}
