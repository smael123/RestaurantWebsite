using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class ArchivableRepository<TArchivableEntity> : IArchivableRepository<TArchivableEntity> where TArchivableEntity : class, IArchivable
    {
        protected readonly DbContext Context;

        public ArchivableRepository(DbContext context) {
            Context = context;
        }

        public void Add(TArchivableEntity entity)
        {
            Context.Set<TArchivableEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TArchivableEntity> entities)
        {
            Context.Set<TArchivableEntity>().AddRange(entities);
        }

        public IEnumerable<TArchivableEntity> Find(Expression<Func<TArchivableEntity, bool>> predicate)
        {
            return Context.Set<TArchivableEntity>().Where(predicate);
        }

        //we will keep this and restrict the visibility of archived 
        public TArchivableEntity Get(int id)
        {
            return Context.Set<TArchivableEntity>().Find(id);
        }

        public IEnumerable<TArchivableEntity> GetAll()
        {
            return Context.Set<TArchivableEntity>()
                .Where(c => !c.IsArchived)
                .ToList();
        }

        public IEnumerable<TArchivableEntity> GetAllArchived()
        {
            return Context.Set<TArchivableEntity>()
                .Where(c => c.IsArchived)
                .ToList();
        }

        public void Remove(TArchivableEntity entity)
        {
            Context.Set<TArchivableEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TArchivableEntity> entities)
        {
            Context.Set<TArchivableEntity>().RemoveRange(entities);
        }

        public TArchivableEntity SingleOrDefault(Expression<Func<TArchivableEntity, bool>> predicate)
        {
            return Context.Set<TArchivableEntity>().SingleOrDefault(predicate);
        }
    }
}