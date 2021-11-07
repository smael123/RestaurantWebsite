using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface IFoodRepository : IRepository<Food>, IArchivableRepository<Food>
    {
        Task<List<Food>> GetAllWithExtras();
        Task<Food> GetWithExtra(int id);
        Task<Food> GetFoodForDetails(int id);
        Task<Food> GetFoodForEdit(int id);
        Task<List<Food>> GetAllForIndex();
        Task<List<Food>> GetAllForAdminIndex();
    }
}
