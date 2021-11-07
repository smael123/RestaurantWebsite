using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Threading.Tasks;

namespace RestaurantWebsite.Persistence.Repositories
{
    public class FoodRepository : ArchivableRepository<Food>, IFoodRepository
    {
        public FoodRepository(RestaurantContext context) : base(context)
        { }

        public RestaurantContext RestaurantContext {
            get { return Context as RestaurantContext; }
        }

        public Task<List<Food>> GetAllWithExtras() {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .ToListAsync();
        }

        public Task<Food> GetWithExtra(int id) {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<Food> GetFoodForDetails(int id)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .Include(c => c.FoodPictures)
                .SingleOrDefaultAsync(c => c.Id == id && c.IsArchived == false);    
        }

        public Task<Food> GetFoodForEdit(int id)
        {
            return RestaurantContext.Foods
                .Include(c => c.Extras)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Food>> GetAllForIndex()
        {
            return RestaurantContext.Foods
                .Include(c => c.FoodPictures)
                .Where(c => c.IsArchived == false)
                .ToListAsync();
        }

        public Task<List<Food>> GetAllForAdminIndex()
        {
            return RestaurantContext.Foods
                .Include(c => c.FoodPictures)
                .ToListAsync();
        }
    }
}