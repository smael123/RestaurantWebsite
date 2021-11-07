using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantWebsite.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;

        public IFoodRepository Foods { get; private set; }
        public IExtraRepository Extras { get; private set; }
        public ISpecialRepository Specials { get; private set; }
        public IFoodPictureRepository FoodPictures { get; private set; }

        public UnitOfWork() {
            _context = new RestaurantContext();

            Foods = new FoodRepository(_context);
            Extras = new ExtraRepository(_context);
            Specials = new SpecialRepository(_context);
            FoodPictures = new FoodPictureRepository(_context);
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}