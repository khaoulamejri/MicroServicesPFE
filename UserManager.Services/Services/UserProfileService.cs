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
    public class UserProfileService : IUserProfileService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly IUserServices UserServices;
        private readonly IProfileServices ProfileServices;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IProfileServices profileServices, IUserServices userServices)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
            ProfileServices = profileServices;
            UserServices = userServices;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public UserProfile Create(UserProfile userProfile)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                if (!utOfWork.UserProfileRepository.GetMany(c => c.ProfileId == userProfile.ProfileId && c.UserName == userProfile.UserName && c.companyID == currentCompanyId).Any())
                {
                    var profile = ProfileServices.GetProfileByID(userProfile.ProfileId);
                    var user = UserServices.GetUserByUserName(userProfile.UserName);
                    if (profile != null && user != null)
                        userProfile.ProfileId = profile.Id;
                    userProfile.UserName = user.UserName;
                    userProfile.companyID = currentCompanyId;
                    utOfWork.UserProfileRepository.Add(userProfile);
                    utOfWork.Commit();
                    return userProfile;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string Delete(int ProfileId, string UserName)
        {
            try
            {
                var userProfile = utOfWork.UserProfileRepository.Get(pc => pc.ProfileId == ProfileId && pc.UserName == UserName);
                if (userProfile != null)
                {
                    utOfWork.UserProfileRepository.Delete(userProfile);
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


        public UserProfile Edit(UserProfile userProfile)
        {
            try
            {
                utOfWork.UserProfileRepository.Update(userProfile);
                utOfWork.Commit();
                return userProfile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.UserProfileRepository.GetMany(c => c.companyID == currentCompanyId).Include(x => x.Profile).Include(p => p.User).ToList();
        }
        public List<UserProfile> GetAllUsersByProfileId(int idProfile)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.UserProfileRepository.GetMany(c => c.companyID == currentCompanyId && c.ProfileId == idProfile).Include(x => x.Profile).Include(p => p.User).ToList();
        }

        public UserProfile GetUserProfileById(int id)
        {
            return utOfWork.UserProfileRepository.Get(a => a.Id == id);
        }

        public List<UserProfile> GetUserProfilesByprofileId(int profileId)
        {
            return utOfWork.UserProfileRepository.GetMany(d => d.ProfileId == profileId).ToList();

        }

        public List<UserProfile> GetUserProfilesByUsername(string username)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.UserProfileRepository.GetMany(d => d.UserName == username && d.companyID == currentCompanyId).Include(x => x.Profile).Include(p => p.User).ToList();
        }
    }
}
