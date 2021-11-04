using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IProfileServices
    {
        Profile Create(Profile Profile);
        Profile Edit(Profile Profile);
        string Delete(int ProfileId);
        Profile GetProfileByID(int ProfileId);
        List<Profile> GetAllProfiles();
        Profile GetProfileByIntitule(string intitule);

    }
}
