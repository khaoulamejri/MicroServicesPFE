using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Data.Repositories
{
public    class DocumentsDepensesRepository : RepositoryBase<DocumentsDepenses>, IDocumentsDepensesRepository
    {
        public DocumentsDepensesRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IDocumentsDepensesRepository : IRepository<DocumentsDepenses> { }
}
