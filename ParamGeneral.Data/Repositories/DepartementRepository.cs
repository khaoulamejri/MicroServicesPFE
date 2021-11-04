using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
 public class DepartementRepository : RepositoryBase<Departement>, IDepartementRepository
    {
        public DepartementRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IDepartementRepository : IRepository<Departement> { }
}
