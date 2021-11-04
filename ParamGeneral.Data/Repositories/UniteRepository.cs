using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
  public  class UniteRepository : RepositoryBase<Unite>, IUniteRepository
    {
        public UniteRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IUniteRepository : IRepository<Unite> { }
}
