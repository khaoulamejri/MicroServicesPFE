using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext dataContext;
        private DbSet<T> dbset;
        IDatabaseFactory databaseFactory;

        protected RepositoryBase(IDatabaseFactory dbFactory)
        {
            this.databaseFactory = dbFactory;
            dbset = (DbSet<T>)DataContext.Set<T>();
        }
        protected ApplicationDbContext DataContext
        {
            get { return dataContext = databaseFactory.DataContext; }
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public Task CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {

            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects) dbset.Remove(obj);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        //public T GetAll(Expression<Func<T, bool>> where)
        //{
        //    return dbset.Where(where).FirstOrDefault<T>();
        //}

        public IQueryable<T> GetAll()
        {
            return dbset;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await dbset.Where(filter).ToListAsync();
            //throw new NotImplementedException();
            //  return await dbset.Find(dataContext).ToListAsync();
            //return (IReadOnlyCollection<T>)Task.FromResult(filter != null ? dataContext.Set<T>().Where(filter) : dataContext.Set<T>());
        }

        public Task<T> GetAsync(int id)
        {
            // return dbset.Find(id);
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return (Task<T>)dbset.Where(where);

        }

        public T GetById(int Id)
        {
            return dbset.Find(Id);
        }

        public T GetById(string Id)
        {
            return dbset.Find(Id);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
