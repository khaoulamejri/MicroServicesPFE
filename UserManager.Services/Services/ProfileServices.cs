using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class ProfileServices : IProfileServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IProfileComposantService pcomp)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public Profile Create(Profile Profile)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                Profile.companyID = currentCompanyId;
                utOfWork.ProfileRepository.Add(Profile);
                utOfWork.Commit();
                return Profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Delete(int ProfileId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var profile = utOfWork.ProfileRepository.GetMany(p => p.Id == ProfileId).Include(x => x.profileComposants).Include(x => x.UserProfile).SingleOrDefault();
                    if (profile != null)
                    {
                        if (profile.UserProfile.Any())
                            return "no";
                        foreach (var item in profile.profileComposants)
                        {
                            utOfWork.ProfileComposantRepository.Delete(item);
                            utOfWork.Commit();
                        }
                        utOfWork.ProfileRepository.Delete(profile);
                        utOfWork.Commit();
                        scope.Complete();
                        return "ok";
                    }
                    else
                    {
                        return "no";
                    }
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        public Profile Edit(Profile Profile)
        {
            try
            {
                utOfWork.ProfileRepository.Update(Profile);
                utOfWork.Commit();
                return Profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Profile> GetAllProfiles()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ProfileRepository.GetMany(p => p.companyID == currentCompanyId).ToList();
        }

        public Profile GetProfileByID(int ProfileId)
        {
            return utOfWork.ProfileRepository.Get(a => a.Id == ProfileId);
        }

        public Profile GetProfileByIntitule(string Intitule)
        {
            return utOfWork.ProfileRepository.Get(p => p.Intitule == Intitule);
        }
    }
}
