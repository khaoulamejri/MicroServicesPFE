using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
  public  class TypeHierarchyPositionRepository : RepositoryBase<TypeHierarchyPosition>, ITypeHierarchyPositionRepository
    {
        public TypeHierarchyPositionRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface ITypeHierarchyPositionRepository : IRepository<TypeHierarchyPosition> { }
}
