using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IFoodPictureRepository : IRepository<FoodPicture>
    {
        IEnumerable<FoodPicture> GetFoodPicturesOfFood(int foodId);
        FoodPicture GetFirstPictureOfFood(int foodId);
    }
}