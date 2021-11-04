using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Repositories
{
    class SeuilRepository : RepositoryBase<Seuils>, ISeuilRepository
    {
        public SeuilRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface ISeuilRepository : IRepository<Seuils> { }
}
