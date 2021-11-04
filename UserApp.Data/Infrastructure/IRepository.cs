using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int Id); T GetById(string Id);
        //  T GetAll(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        Task CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);

    }
}