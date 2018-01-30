using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;

        public IFoodRepository Foods { get; private set; }
        public IExtraRepository Extras { get; private set; }
        public ISpecialRepository Specials { get; private set; }

        public UnitOfWork(RestaurantContext context) {
            _context = context;
            Foods = new FoodRepository(_context);
            Extras = new ExtraRepository(_context);
            Specials = new SpecialRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}