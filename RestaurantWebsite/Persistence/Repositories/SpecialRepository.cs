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
    public class SpecialRepository : ArchivableRepository<Special>, ISpecialRepository
    {
        public SpecialRepository(RestaurantContext context) : base(context) { }

        public RestaurantContext RestaurantContext
        {
            get { return Context as RestaurantContext; }
        }

        public Task<List<Special>> GetCurrentSpecials()
        {
            return RestaurantContext.Specials
                .Where(c => c.EndDate.CompareTo(DateTime.Now) >= 0)
                .ToListAsync();
        }

        public Task<List<Special>> GetAllWithFoods() {
            return RestaurantContext.Specials
                .Include(c => c.FoodsOnSpecial)
                .ToListAsync();
        }

        public Task<Special> GetWithFood(int id) {
            return RestaurantContext.Specials
                .Include(c => c.FoodsOnSpecial)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Special>> GetAllForIndex() {
            return RestaurantContext.Specials
                .Where(c => c.IsArchived == false)
                .ToListAsync();
        }

        public Task<List<Special>> GetAllForAdminIndex()
        {
            return RestaurantContext.Specials
                .ToListAsync();
        }
    }
}