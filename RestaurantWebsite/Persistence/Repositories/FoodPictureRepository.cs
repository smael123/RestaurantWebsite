using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class FoodPictureRepository : Repository<FoodPicture>, IFoodPictureRepository
    {
        public FoodPictureRepository(RestaurantContext context) : base(context)
        { }

        public RestaurantContext RestaurantContext
        {
            get { return Context as RestaurantContext; }
        }

        public FoodPicture GetFirstPictureOfFood(int foodId)
        {
            return RestaurantContext
                .FoodPictures
                .FirstOrDefault(c => c.FoodId == foodId); 
        }

        public IEnumerable<FoodPicture> GetFoodPicturesOfFood(int foodId)
        {
            return RestaurantContext
                .FoodPictures
                .Where(c => c.FoodId == foodId)
                .ToList();
        }
    }
}