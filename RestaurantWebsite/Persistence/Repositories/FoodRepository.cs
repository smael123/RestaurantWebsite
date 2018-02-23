using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        public FoodRepository(RestaurantContext context) : base(context)
        { }

        public RestaurantContext RestaurantContext {
            get { return Context as RestaurantContext; }
        }

        //public IEnumerable<Food> GetExtrasOfFood()
        //{
        //    return RestaurantContext.Foods
        //        .Include(c => c.Extras)
        //        .Select(c => c.Extras)
        //        .ToList();
        //}

        public IEnumerable<Food> GetAllWithExtras() {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .ToList();
        }

        public Food GetWithExtra(int id) {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .SingleOrDefault(c => c.Id == id);
        }

        //public Food SingleOrDefaultWithExtras(int id)
        //{
        //    return RestaurantContext.Foods
        //        .Include(c => c.Extras)
        //}

        public IEnumerable<Food> GetFoodsUpToPrice(decimal maxPrice)
        {
            return RestaurantContext.Foods
                .Where(f => f.BasePrice <= maxPrice).ToList();
        }

        public Food GetWithExtrasAndPictures(int id)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .Include(c => c.FoodPictures)
                .SingleOrDefault(c => c.Id == id);
        }

        //public new void Add(Food food) {
        //    RestaurantContext.Foods
        //        .Add(food);

        //    foreach (var extra in food.Extras) {
        //        RestaurantContext.Extras
        //            .Add(extra);
        //    }
        //}

    }
}