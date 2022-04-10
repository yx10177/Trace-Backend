using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trace.Repository
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        readonly ApplicationDbContext _context;
        readonly DbSet<TEntity> _dbSet;
        public BaseRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
            _dbSet = _context.Set<TEntity>();
        }

        public void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            _dbSet.Remove(entityToDelete);
        }
        public void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            if (entitiesToDelete == null) throw new ArgumentNullException(nameof(entitiesToDelete));
            _dbSet.RemoveRange(entitiesToDelete);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null) return _dbSet;
            else return _dbSet.Where(filter);
        }
        public void Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
        }
        public void InsertRange(IEnumerable<TEntity> entitys)
        {
            if (entitys == null) throw new ArgumentNullException(nameof(entitys));

            _dbSet.AddRange(entitys);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null) throw new ArgumentNullException(nameof(entityToUpdate));

            _dbSet.Update(entityToUpdate);
        }
    }
}
