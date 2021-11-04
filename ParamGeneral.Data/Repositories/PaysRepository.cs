using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
 public   class PaysRepository : RepositoryBase<Pays>, IPaysRepository
    {
        public PaysRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IPaysRepository : IRepository<Pays> { }
}
