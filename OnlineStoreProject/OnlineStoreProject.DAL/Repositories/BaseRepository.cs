using OnlineStoreProject.DAL.EF;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DataContext db;
        private DbSet<TEntity> dbSet;

        public BaseRepository(DataContext _db)
        {
            this.db = _db;
            this.dbSet = db.Set<TEntity>();
        }

        
        public void Add(TEntity item)
        {
            dbSet.Add(item);
            db.SaveChanges();
        }

        public void Delete(TEntity item)
        {
            if (item == null)
                return;
            dbSet.Remove(item);
            db.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public void Update(TEntity item)
        {
            dbSet.Attach(item);
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func <TEntity, bool> predicate, params Expression<Func <TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
