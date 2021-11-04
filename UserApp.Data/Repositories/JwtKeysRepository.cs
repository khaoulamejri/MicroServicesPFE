using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;

namespace UserApp.Data.Repositories
{
 public   class JwtKeysRepository : RepositoryBase<JwtKeys>, IJwtKeysRepository
    {
        public JwtKeysRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IJwtKeysRepository : IRepository<JwtKeys> { }
}