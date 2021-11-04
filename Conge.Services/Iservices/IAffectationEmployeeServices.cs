using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IAffectationEmployeeServices
    {
        AffectationEmployee GetAffectationActif(int EmployeeId);
        List<AffectationEmployee> GetAffectationEmployeeByEmployeeId(int id);
        AffectationEmployee GetAffectationEmployeeByID(int id);
        AffectationEmployee Create(AffectationEmployee AffectationEmployee);
    }
}
