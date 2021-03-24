using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie.API.Data.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> newEntities);
        void DeleteEntity(T entity);

        Task<IEnumerable<T>> GetAllByFilter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includProperties = null);

        Task<T> FirstOrDefaultrFilter(
            Expression<Func<T, bool>> filter = null,
            string includProperties = null);
        Task<IEnumerable<T>> GetAll();


    }
}
