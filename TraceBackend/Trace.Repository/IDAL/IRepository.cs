using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trace.Repository
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entitys);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);
        void SaveChanges();
        Task SaveChangesAsync();

        DatabaseFacade GetDatabase();
    }
}
