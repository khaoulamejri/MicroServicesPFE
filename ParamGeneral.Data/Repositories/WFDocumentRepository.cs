using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Repositories
{
    public class WFDocumentRepository : RepositoryBase<WFDocument>, IWFDocumentRepository
    {
        public WFDocumentRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IWFDocumentRepository : IRepository<WFDocument> { }
}
