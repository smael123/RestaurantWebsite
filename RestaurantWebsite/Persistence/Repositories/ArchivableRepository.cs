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
    public class ArchivableRepository<TArchivableEntity> : Repository<TArchivableEntity>, IArchivableRepository<TArchivableEntity> where TArchivableEntity : class, IArchivable
    {
        public ArchivableRepository(DbContext context) : base(context) { }

        public TArchivableEntity GetUnarchived(int id)
        {
            TArchivableEntity entity = Context.Set<TArchivableEntity>().Find(id);

            return (entity.IsArchived) ? null : entity;
        }

        public IEnumerable<TArchivableEntity> GetAllUnArchived()
        {
            return Context.Set<TArchivableEntity>().Where(c => c.IsArchived == false);
        }

        public TArchivableEntity GetArchived(int id)
        {
            TArchivableEntity entity = Context.Set<TArchivableEntity>().Find(id);

            return (entity.IsArchived) ? entity : null;
        }

        public IEnumerable<TArchivableEntity> GetAllArchived()
        {
            return Context.Set<TArchivableEntity>().Where(c => c.IsArchived == true);
        }
    }
}