using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Repositories;

namespace UserApp.Data.Infrastructure
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
       
        public IRolesPrivilegesRepository rolesPrivilegesRepository;
        public IRolesPrivilegesRepository RolesPrivilegesRepository
        {
            get { return rolesPrivilegesRepository = new RolesPrivilegesRepository(dbFactory); }
        }

        public IProfileComposantRepository profileComposantRepository ;
        public IProfileComposantRepository ProfileComposantRepository
        {
            get { return profileComposantRepository = new ProfileComposantRepository(dbFactory); }
        }

        public IProfileRepository profileRepository ;
        public IProfileRepository ProfileRepository
        {
            get { return profileRepository = new ProfileRepository(dbFactory); }
        }

        public IUserProfileRepository userProfileRepository;
        public IUserProfileRepository UserProfileRepository
        {
            get { return userProfileRepository = new UserProfileRepository(dbFactory); }
        }
        public IUserRepository userRepository ;
        public IUserRepository UserRepository
        {
            get { return userRepository = new UserRepository(dbFactory); }
        }

        public IUserRoleCompaniesRepository userRoleCompaniesRepository;
        public IUserRoleCompaniesRepository UserRoleCompaniesRepository
        {
            get { return userRoleCompaniesRepository = new UserRoleCompaniesRepository(dbFactory); }
        }
        public IUserRoleRepository userRoleRepository ;
        public IUserRoleRepository UserRoleRepository
        {
            get { return userRoleRepository = new UserRoleRepository(dbFactory); }
        }

        public IComposantRepository composantRepository ;
        public IComposantRepository ComposantRepository
        {
            get { return composantRepository = new ComposantRepository(dbFactory); }
        }

        public ICompanyRepository companyRepository ;
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

        public IEmployeeRepository employeeRepository ;
        public IEmployeeRepository EmployeeRepository
        {
            get { return employeeRepository = new EmployeeRepository(dbFactory); }
        }

        public IJwtKeysRepository jwtKeysRepository ;
        public IJwtKeysRepository JwtKeysRepository
        {
            get { return jwtKeysRepository = new JwtKeysRepository(dbFactory); }
        }

        public void Commit() { DataContext.SaveChanges(); }
        public void Dispose()
        {
            dbFactory.Dispose();
        }
    }
}
