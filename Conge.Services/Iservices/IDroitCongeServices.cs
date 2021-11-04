using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IDroitCongeServices
    {
        List<DroitConge> GetAllDroitConge();
        DroitConge Create(DroitConge droitConge);
        DroitConge GetDroitCongeByID(int id);
        List<DroitConge> GetDroitCongeByDate(DateTime date);
        DroitConge Edit(DroitConge DroitConge);
    }
}
