using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class SpecialRepository : Repository<Special>, ISpecialRepository
    {
        public SpecialRepository(RestaurantContext context) : base(context) { }

        public RestaurantContext RestaurantContext
        {
            get { return Context as RestaurantContext; }
        }

        //public IEnumerable<Special> GetAllSpecialsWithFood(int id, int foodId)
        //{
        //    var specialInDb = this.Find(c => c.Id == id);

        //    var foodInDb = specialInDb
        //        .SelectMany(c => c.FoodsOnSpecial)
        //        .Where(c => c.Id == foodId).ToList();
        //}

        public IEnumerable<Special> GetCurrentSpecials()
        {
            return RestaurantContext.Specials
                .Where(c => c.EndDate.CompareTo(DateTime.Now) >= 0)
                .ToList();
        }

        public IEnumerable<Special> GetOldSpecials()
        {
            return RestaurantContext.Specials
                .Where(c => c.EndDate.CompareTo(DateTime.Now) < 0)
                .ToList();
        }

        public IEnumerable<Special> GetAllWithFoods() {
            return RestaurantContext.Specials
                .Include(c => c.FoodsOnSpecial)
                .ToList();
        }

        public Special GetWithFood(int id) {
            return RestaurantContext.Specials
                .Include(c => c.FoodsOnSpecial)
                .SingleOrDefault(c => c.Id == id);
        } 
    }
}