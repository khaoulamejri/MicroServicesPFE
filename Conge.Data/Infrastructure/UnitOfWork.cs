using Conge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Infrastructure
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
        private IAncienteRepository ancienteRepository;
        public IAncienteRepository AncienteRepository
        {
            get { return ancienteRepository = new AncienteRepository(dbFactory); }
        }
        private ITypeCongeRepository typeCongeRepository;
        public ITypeCongeRepository TypeCongeRepository
        {
            get { return typeCongeRepository = new TypeCongeRepository(dbFactory); }
        }
        private ISoldeCongeRepository soldeCongeRepository;
        public ISoldeCongeRepository SoldeCongeRepository
        {
            get { return soldeCongeRepository = new SoldeCongeRepository(dbFactory); }
        }
        private IPlanDroitCongeRepository planDroitCongeRepository;
        public IPlanDroitCongeRepository PlanDroitCongeRepository
        {
            get { return planDroitCongeRepository = new PlanDroitCongeRepository(dbFactory); }
        }
        private IDemandeCongeRepository demandeCongeRepository;
        public IDemandeCongeRepository DemandeCongeRepository
        {
            get { return demandeCongeRepository = new DemandeCongeRepository(dbFactory); }
        }
        private IDroitCongeRepository droitCongeRepository;
        public IDroitCongeRepository DroitCongeRepository
        {
            get { return droitCongeRepository = new DroitCongeRepository(dbFactory); }
        }
        private IDetailsDroitCongeRepository detailsDroitCongeRepository;
        public IDetailsDroitCongeRepository DetailsDroitCongeRepository
        {
            get { return detailsDroitCongeRepository = new DetailsDroitCongeRepository(dbFactory); }
        }
        private IJoursFeriesRepository joursFeriesRepository;
        public IJoursFeriesRepository JoursFeriesRepository
        {
            get { return joursFeriesRepository = new JoursFeriesRepository(dbFactory); }
        }
        private IMvtCongeRepository mvtCongeRepository;
        public IMvtCongeRepository MvtCongeRepository
        {
            get { return mvtCongeRepository = new MvtCongeRepository(dbFactory); }
        }
        private ITitreCongeRepository titreCongeRepository;
        public ITitreCongeRepository TitreCongeRepository
        {
            get { return titreCongeRepository = new TitreCongeRepository(dbFactory); }
        }
        private IDocumentsCongeRepository documentsCongeRepository;
        public IDocumentsCongeRepository DocumentsCongeRepository
        {
            get { return documentsCongeRepository = new DocumentsCongeRepository(dbFactory); }
        }
        private IDelegationRepository delegationRepository;
        public IDelegationRepository DelegationRepository
        {
            get { return delegationRepository = new DelegationRepository(dbFactory); }
        }
        public IEmployeeRepository employeeRepository;
        public IEmployeeRepository EmployeeRepository
        {
            get { return employeeRepository = new EmployeeRepository(dbFactory); }
        }
        public ICompanyRepository companyRepository;
        public ICompanyRepository CompanyRepository
        {
            get { return companyRepository = new CompanyRepository(dbFactory); }
        }

        public ISeuilRepository seuilRepository ;
        public ISeuilRepository SeuilRepository
        {
            get { return seuilRepository = new SeuilRepository(dbFactory); }
        }

        public IAffectationEmployeeRepository affectationEmployeeRepository ;
        public IAffectationEmployeeRepository AffectationEmployeeRepository
        {
            get { return affectationEmployeeRepository = new AffectationEmployeeRepository(dbFactory); }
        }

        public IUniteRepository uniteRepository ;
        public IUniteRepository UniteRepository
        {
            get { return uniteRepository = new UniteRepository(dbFactory); }
        }
        public void Commit() { DataContext.SaveChanges(); }
        public void Dispose()
        {
            dbFactory.Dispose();
        }
    }
}
