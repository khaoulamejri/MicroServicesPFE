using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;

namespace UserApp.Data.Repositories
{
  public  class ComposantRepository : RepositoryBase<Composant>, IComposantRepository
    {
        public ComposantRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IComposantRepository : IRepository<Composant> { }
}
