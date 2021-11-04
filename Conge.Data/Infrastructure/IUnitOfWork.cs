using Conge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

      
      
      
        IAncienteRepository AncienteRepository { get; }
        ITypeCongeRepository TypeCongeRepository { get; }
        ISoldeCongeRepository SoldeCongeRepository { get; }
        IPlanDroitCongeRepository PlanDroitCongeRepository { get; }
        IDemandeCongeRepository DemandeCongeRepository { get; }
        IDroitCongeRepository DroitCongeRepository { get; }
        IDetailsDroitCongeRepository DetailsDroitCongeRepository { get; }
        IJoursFeriesRepository JoursFeriesRepository { get; }
        IMvtCongeRepository MvtCongeRepository { get; }
        ITitreCongeRepository TitreCongeRepository { get; }
        IDocumentsCongeRepository DocumentsCongeRepository { get; }
        IDelegationRepository DelegationRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ISeuilRepository SeuilRepository { get; }
        IAffectationEmployeeRepository AffectationEmployeeRepository { get; }
        IUniteRepository UniteRepository { get; }
    }
}
