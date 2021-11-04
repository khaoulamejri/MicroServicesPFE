using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IJoursFeriesServices
    {
        List<JoursFeries> GetAllJoursFeries();
        JoursFeries Create(JoursFeries joursFeries);
        JoursFeries GetJoursFeriesByID(int id);
        JoursFeries Edit(JoursFeries joursFeries);
        JoursFeries Delete(int JoursFeriesId);
        bool CheckUnicity(JoursFeries joursFeries, int id);
        bool CheckUnicity(JoursFeries joursFeries);
    }
}
