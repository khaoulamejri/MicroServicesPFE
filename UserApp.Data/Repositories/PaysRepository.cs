using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;

namespace UserApp.Data.Repositories
{
    public class PaysRepository : RepositoryBase<Pays>, IPaysRepository
    {
        public PaysRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IPaysRepository : IRepository<Pays> { }
}
