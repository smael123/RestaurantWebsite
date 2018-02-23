using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IFoodRepository : IRepository<Food>
    {
        IEnumerable<Food> GetFoodsUpToPrice(decimal maxPrice);
        IEnumerable<Food> GetAllWithExtras();
        Food GetWithExtrasAndPictures(int id);
        Food GetWithExtra(int id);
        //IEnumerable<Food> GetExtrasOfFood()
        //IEnumerable<Food> GetFoodsOfSpecials(int specialId);
    }
}
