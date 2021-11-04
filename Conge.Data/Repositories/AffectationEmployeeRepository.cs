using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Repositories
{
    public class AffectationEmployeeRepository : RepositoryBase<AffectationEmployee>, IAffectationEmployeeRepository
    {
        public AffectationEmployeeRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IAffectationEmployeeRepository : IRepository<AffectationEmployee> { }
}