using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IFoodPictureRepository : IRepository<FoodPicture>
    {
        Task<List<FoodPicture>> GetFoodPicturesOfFood(int foodId);
        Task<FoodPicture> GetFirstPictureOfFood(int foodId);
    }
}