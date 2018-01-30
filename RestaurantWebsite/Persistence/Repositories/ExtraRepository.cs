using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class ExtraRepository : Repository<Extra>, IExtraRepository
    {
        public ExtraRepository(RestaurantContext context) : base(context) { }

        public RestaurantContext RestaurantContext
        {
            get { return Context as RestaurantContext; }
        }

        public IEnumerable<Extra> GetExtrasOfFood(int foodId)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .SelectMany(c => c.Extras)
                .ToList();
        }
    }
}