using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Repositories
{
    public  class PlanDroitCongeRepository : RepositoryBase<PlanDroitConge>, IPlanDroitCongeRepository
    {
        public PlanDroitCongeRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IPlanDroitCongeRepository : IRepository<PlanDroitConge> { }
}
