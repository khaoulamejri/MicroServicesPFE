using ParamGeneral.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();




        IAffectationEmployeeRepository AffectationEmployeeRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IPositionRepository PositionRepository { get; }
        IDepartementRepository DepartementRepository { get; }
        IEmploiRepository EmploiRepository { get; }
        IHierarchyPositionRepository HierarchyPositionRepository { get; }
        IParamGenerauxRepository ParamGenerauxRepository { get; }
        IRegimeTravailRepository RegimeTravailRepository { get; }
        ITypeHierarchyPositionRepository TypeHierarchyPositionRepository { get; }
        IUniteRepository UniteRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IDeviseRepository DeviseRepository { get; }
        IPaysRepository PaysRepository { get; }
        IEmployeeVehiculeRepository EmployeeVehiculeRepository { get; }
        IWFDocumentRepository WFDocumentRepository { get; }
    }
}
