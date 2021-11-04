using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IUserProfileService
    {
        UserProfile Create(UserProfile userProfile);
        UserProfile Edit(UserProfile userProfile);
        string Delete(int ProfileId, string UserName);
        UserProfile GetUserProfileById(int id);
        List<UserProfile> GetUserProfilesByprofileId(int profileId);
        List<UserProfile> GetUserProfilesByUsername(string username);
        List<UserProfile> GetAllUserProfiles();
        List<UserProfile> GetAllUsersByProfileId(int profileId);

    }

}