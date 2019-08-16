using System;
using TARS.DataModel.GenericRepository.Abstract;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using TARS.DataModel.DataModel;
using System.Data.Entity;

namespace TARS.DataModel.GenericRepository.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        internal TARSEntities Context;

        internal DbSet<TEntity> DbSet;

        #region public constructor...

        public Repository(TARSEntities contextTars)
        {
            this.Context = contextTars;
            this.DbSet = Context.Set<TEntity>();
        }
        #endregion


        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbSet;
            return query.Where(predicate).ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }
            DbSet.Remove(entityToDelete);
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
           DbSet.RemoveRange(entity);
        }

    }
}
