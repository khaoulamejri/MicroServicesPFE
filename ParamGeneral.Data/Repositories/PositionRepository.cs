using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
   public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IPositionRepository : IRepository<Position> { }
}
