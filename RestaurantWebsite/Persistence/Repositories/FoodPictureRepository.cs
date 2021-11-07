using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<FoodPicture> GetFirstPictureOfFood(int foodId)
        {
            return RestaurantContext
                .FoodPictures
                .FirstOrDefaultAsync(c => c.FoodId == foodId); 
        }

        public Task<List<FoodPicture>> GetFoodPicturesOfFood(int foodId)
        {
            return RestaurantContext
                .FoodPictures
                .Where(c => c.FoodId == foodId)
                .ToListAsync();
        }
    }
}