using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface ISpecialRepository : IRepository<Special>
    {
        //IEnumerable<Special> GetAllSpecialsWithFood(int id, int foodId);
        IEnumerable<Special> GetCurrentSpecials();
        IEnumerable<Special> GetOldSpecials();
        IEnumerable<Special> GetAllWithFoods();
        Special GetWithFood(int id);
        //Special GetWithPicture(int id);
        //Special GetWithNavigationalProperties(int id);
    }
}
