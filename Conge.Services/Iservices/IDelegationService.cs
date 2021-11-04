using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface IDelegationService
    {
        List<Delegation> GetAllDelegations();
        Delegation GetDelegationById(int id);
        Delegation GetDelegationByTitre(int id);
        List<Delegation> GetDelegationByEmployeeId(int id);
        List<Delegation> GetDelegationByRemplaçantId(int id);
        bool hasDelegation(int idRemplacant);
        int getSubstituter(int employee);
        Delegation getDelegation(int employee);
        Delegation getDelegationByRemplacant(int idRemplacant);

        int getDelegator(int remplacant);
        Delegation Create(Delegation delegation);
        Delegation Edit(Delegation delegation);
    }
}

