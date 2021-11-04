using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Repositories
{
   public class DetailsDroitCongeRepository : RepositoryBase<Details_DroitConge>, IDetailsDroitCongeRepository
    {
        public DetailsDroitCongeRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IDetailsDroitCongeRepository : IRepository<Details_DroitConge> { }
}
