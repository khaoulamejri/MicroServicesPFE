using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Repositories;

namespace UserApp.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRolesPrivilegesRepository RolesPrivilegesRepository { get; }
        IProfileComposantRepository ProfileComposantRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleCompaniesRepository UserRoleCompaniesRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IComposantRepository ComposantRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IDeviseRepository DeviseRepository { get; }
        IPaysRepository PaysRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IJwtKeysRepository JwtKeysRepository { get; }
    }
}
