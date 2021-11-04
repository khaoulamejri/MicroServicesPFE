using NoteDeFrais.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext dataContext;
        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;

        }

        public IPaysRepository paysRepository ;
        public IPaysRepository PaysRepository
        {
            get { return paysRepository = new PaysRepository(dbFactory); }
        }

        public ICompteComptableRepository compteComptableRepository ;
        public ICompteComptableRepository CompteComptableRepository
        {
            get { return compteComptableRepository = new CompteComptableRepository(dbFactory); }
        }
        public IDepensesRepository depensesRepository;
        public IDepensesRepository DepensesRepository
        {
            get { return depensesRepository = new DepensesRepository(dbFactory); }
        }

        public IDeviseRepository deviseRepository ;
        public IDeviseRepository DeviseRepository
        {
            get { return deviseRepository = new DeviseRepository(dbFactory); }
        }
        public IDocumentsDepensesRepository documentsDepensesRepository ;
        public IDocumentsDepensesRepository DocumentsDepensesRepository
        {
            get { return documentsDepensesRepository = new DocumentsDepensesRepository(dbFactory); }
        }
        public IDocumentsNoteFraisRepository documentsNoteFraisRepository ;
        public IDocumentsNoteFraisRepository DocumentsNoteFraisRepository
        {
            get { return documentsNoteFraisRepository = new DocumentsNoteFraisRepository(dbFactory); }
        }
        public IEmployeeGroupeRepository employeeGroupeRepository ;
        public IEmployeeGroupeRepository EmployeeGroupeRepository
        {
            get { return employeeGroupeRepository = new EmployeeGroupeRepository(dbFactory); }
        }
        public IEmployeeRepository employeeRepository ;
        public IEmployeeRepository EmployeeRepository
        {
            get { return employeeRepository = new EmployeeRepository(dbFactory); }
        }
        public IFraisKilometriquesRepository fraisKilometriquesRepository ;
        public IFraisKilometriquesRepository FraisKilometriquesRepository
        {
            get { return fraisKilometriquesRepository = new FraisKilometriquesRepository(dbFactory); }
        }
        public IGroupeFraisDepenseRepository groupeFraisDepenseRepository;
        public IGroupeFraisDepenseRepository GroupeFraisDepenseRepository
        {
            get { return groupeFraisDepenseRepository = new GroupeFraisDepenseRepository(dbFactory); }
        }
        public IGroupeFraisRepository groupeFraisRepository ;
        public IGroupeFraisRepository GroupeFraisRepository
        {
            get { return groupeFraisRepository = new GroupeFraisRepository(dbFactory); }
        }
        public IMoyenPaiementRepository moyenPaiementRepository;
        public IMoyenPaiementRepository MoyenPaiementRepository
        {
            get { return moyenPaiementRepository = new MoyenPaiementRepository(dbFactory); }
        }
        public INoteFraisRepository noteFraisRepository ;
        public INoteFraisRepository NoteFraisRepository
        {
            get { return noteFraisRepository = new NoteFraisRepository(dbFactory); }
        }
        public IOrdreMissionRepository ordreMissionRepository ;
        public IOrdreMissionRepository OrdreMissionRepository
        {
            get { return ordreMissionRepository = new OrdreMissionRepository(dbFactory); }
        }
        public ITypeDepenseRepository typeDepenseRepository ;
        public ITypeDepenseRepository TypeDepenseRepository
        {
            get { return typeDepenseRepository = new TypeDepenseRepository(dbFactory); }
        }
        public ITypeOrdreMissionRepository typeOrdreMissionRepository ;
        public ITypeOrdreMissionRepository TypeOrdreMissionRepository
        {
            get { return typeOrdreMissionRepository = new TypeOrdreMissionRepository(dbFactory); }
        }
        protected ApplicationDbContext DataContext
        {
            get { return dataContext = dbFactory.DataContext; }
        }

        public IWFDocumentRepository wFDocumentRepository;
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
