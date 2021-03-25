using Microsoft.EntityFrameworkCore;
using Movie.API.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie.API.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> entities;
   
        public Repository(ApplicationContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
            if (entity == null) return;
           await entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> newEntities)
        {
            if(!newEntities.Any()) return;
            await entities.AddRangeAsync(newEntities);
        }

        public void DeleteEntity(T entity)
        {
            if (entity == null) return;
            entities.Remove(entity);
        }

        public async Task<T> FirstOrDefaultrFilter(
            Expression<Func<T, bool>> filter = null, 
            string includProperties = null)
        {
            IQueryable<T> query = entities;
            if(filter is not null)
            {
                
              query = query.Where(filter);
            }
            if(!string.IsNullOrEmpty(includProperties))
            {
                foreach(var property in includProperties.Split(new char[] { ','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                  query =  query.Include(property);
                }
            }
            if (query is null) return null;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByFilter(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includProperties = null)
        {
            IQueryable<T> query = entities;
            if(filter is not null)
            {
              query =  query.Where(filter);
            }
            if(!string.IsNullOrEmpty(includProperties))
            {
                foreach(var property in includProperties.Split(new char[] { ','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                  query =  query.Include(filter);
                }
            }
            if(orderBy is not null)
            {
              return await orderBy(query).ToListAsync();
            }

            return query == null ? query : await query.ToListAsync();
        }
    }
}
