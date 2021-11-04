using ParamGeneral.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext dataContext;
        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;

        }
        protected ApplicationDbContext DataContext
        {
            get { return dataContext = dbFactory.DataContext; }
        }

        public IAffectationEmployeeRepository affectationEmployeeRepository;
        public IAffectationEmployeeRepository AffectationEmployeeRepository
        {
            get { return affectationEmployeeRepository = new AffectationEmployeeRepository(dbFactory); }
        }
        public IEmployeeRepository employeeRepository;
        public IEmployeeRepository EmployeeRepository
        {
            get { return employeeRepository = new EmployeeRepository(dbFactory); }
        }

        public IPositionRepository positionRepository ;
        public IPositionRepository PositionRepository
        {
            get { return positionRepository = new PositionRepository(dbFactory); }
        }

        public IDepartementRepository departementRepository;
       
        public IDepartementRepository DepartementRepository
        {
            get { return departementRepository = new DepartementRepository(dbFactory); }
        }


        public IEmploiRepository emploiRepository;
        public IEmploiRepository EmploiRepository
        {
            get { return emploiRepository = new EmploiRepository(dbFactory); }
        }

        public IHierarchyPositionRepository hierarchyPositionRepository ;
        public IHierarchyPositionRepository HierarchyPositionRepository
        {
            get { return hierarchyPositionRepository = new HierarchyPositionRepository(dbFactory); }
        }

        public IParamGenerauxRepository paramGenerauxRepository ;
        public IParamGenerauxRepository ParamGenerauxRepository
        {
            get { return paramGenerauxRepository = new ParamGenerauxRepository(dbFactory); }
        }

        public IRegimeTravailRepository regimeTravailRepository ;
        public IRegimeTravailRepository RegimeTravailRepository
        {
            get { return regimeTravailRepository = new RegimeTravailRepository(dbFactory); }
        }

        public ITypeHierarchyPositionRepository typeHierarchyPositionRepository;
        public ITypeHierarchyPositionRepository TypeHierarchyPositionRepository
        {
            get { return typeHierarchyPositionRepository = new TypeHierarchyPositionRepository(dbFactory); }
        }

        public IUniteRepository uniteRepository ;
        public IUniteRepository UniteRepository
        {
            get { return uniteRepository = new UniteRepository(dbFactory); }
        }

        public ICompanyRepository companyRepository;
        public ICompanyRepository CompanyRepository
        {
            get { return companyRepository = new CompanyRepository(dbFactory); }
        }

        public IDeviseRepository deviseRepository ;
        public IDeviseRepository DeviseRepository
        {
            get { return deviseRepository = new DeviseRepository(dbFactory); }
        }
        public IPaysRepository paysRepository ;
        public IPaysRepository PaysRepository
        {
            get { return paysRepository = new PaysRepository(dbFactory); }
        }

        public IEmployeeVehiculeRepository employeeVehiculeRepository ;
        public IEmployeeVehiculeRepository EmployeeVehiculeRepository
        {
            get { return employeeVehiculeRepository = new EmployeeVehiculeRepository(dbFactory); }
        }

        public IWFDocumentRepository wFDocumentRepository ;
        public IWFDocumentRepository WFDocumentRepository
        {
            get { return wFDocumentRepository = new WFDocumentRepository(dbFactory); }
        }
        public void Commit() { DataContext.SaveChanges(); }
        public void Dispose()
        {
            dbFactory.Dispose();
        }
    }
}
