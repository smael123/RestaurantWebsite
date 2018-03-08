using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IArchivableRepository<TArchivableEntity> where TArchivableEntity : IArchivable
    {
        TArchivableEntity GetUnarchived(int id);
        IEnumerable<TArchivableEntity> GetAllUnArchived();
        TArchivableEntity GetArchived(int id);
        IEnumerable<TArchivableEntity> GetAllArchived();
    }
}
