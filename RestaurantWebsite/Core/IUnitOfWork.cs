using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IFoodRepository Foods { get; }
        IExtraRepository Extras { get; }
        ISpecialRepository Specials { get; }

        int Complete();
    }
}
