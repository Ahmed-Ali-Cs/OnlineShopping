using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Repositories.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbcontext db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbcontext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? Includeproperty = null)
        {
            IQueryable<T> query = dbSet;
            if (Includeproperty != null)
            {
                foreach (var includeprop in Includeproperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.ToList();
        }

        

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? Includeproperty = null)
        {
            IQueryable<T> query = dbSet;
            if (Includeproperty != null)
            {
                foreach (var includeprop in Includeproperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
