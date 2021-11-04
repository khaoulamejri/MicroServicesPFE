using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ApplicationDbContext dataContext;
        public ApplicationDbContext DataContext
        {
            get { return dataContext; }
        }
        public DatabaseFactory(ApplicationDbContext context)
        {
            dataContext = context;

        }
        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }
}

