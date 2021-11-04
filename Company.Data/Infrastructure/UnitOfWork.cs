using Compank.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compank.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext dataContext;
        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        private ICompanyRepository companyRepository;
        public ICompanyRepository CompanyRepository
        {
            get { return companyRepository = new CompanyRepository(dbFactory); }
        }

        protected ApplicationDbContext DataContext
        {
            get { return dataContext = dbFactory.DataContext; }
        }

        public void Commit() { DataContext.SaveChanges(); }
        public void Dispose()
        {
            dbFactory.Dispose();
        }
    }
}
