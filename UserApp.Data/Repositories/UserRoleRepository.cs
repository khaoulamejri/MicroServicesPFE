using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;

namespace UserApp.Data.Repositories
{
   public class UserRoleRepository : RepositoryBase<ApplicationRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IUserRoleRepository : IRepository<ApplicationRole> { }
}
