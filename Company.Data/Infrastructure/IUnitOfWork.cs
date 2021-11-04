using Compank.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compank.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        ICompanyRepository CompanyRepository { get; }
    }
}
