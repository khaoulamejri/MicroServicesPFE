using Compank.Data.Infrastructure;
using Compank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compank.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Companyk>, ICompanyRepository
    {
        public CompanyRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface ICompanyRepository : IRepository<Companyk> { }
}