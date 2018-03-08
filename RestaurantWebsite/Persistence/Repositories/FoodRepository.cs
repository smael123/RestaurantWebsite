using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class FoodRepository : ArchivableRepository<Food>, IFoodRepository
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

        public Food GetFoodForDetails(int id)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .Include(c => c.FoodPictures)
                .SingleOrDefault(c => c.Id == id && c.IsArchived == false);    
        }

        public Food GetFoodForEdit(int id)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Food> GetAllForIndex()
        {
            return RestaurantContext.Foods
                .Include(c => c.FoodPictures)
                .Where(c => c.IsArchived == false);
        }

        public IEnumerable<Food> GetAllForAdminIndex()
        {
            return RestaurantContext.Foods
                .Include(c => c.FoodPictures);
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