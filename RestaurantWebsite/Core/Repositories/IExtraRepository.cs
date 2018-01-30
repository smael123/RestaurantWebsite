using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IExtraRepository : IRepository<Extra>
    {
        IEnumerable<Extra> GetExtrasOfFood(int foodId);
    }
}
