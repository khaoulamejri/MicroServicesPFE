using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IProfileComposantService
    {
        ProfileComposant Create(ProfileComposant ProfileComposant);
        ProfileComposant Edit(ProfileComposant ProfileComposant);
        string Delete(int ProfileId, int composantId);
        ProfileComposant GetProfileComposantById(int ProfileId, int composantId);
        List<ProfileComposant> GetProfileComposantsByprofileId(int profileId);
        List<ProfileComposant> GetProfileComposantsByComposantId(int composantId);
        List<ProfileComposant> GetAllProfileComposants();



    }
}
