using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Data.Repositories
{
    public class FraisKilometriquesRepository : RepositoryBase<FraisKilometriques>, IFraisKilometriquesRepository
    {
        public FraisKilometriquesRepository(IDatabaseFactory dbFactory) : base(dbFactory) { }
    }
    public interface IFraisKilometriquesRepository : IRepository<FraisKilometriques> { }
}
