using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        TEntity Get(int id);
        void Add(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
    params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
