using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Repositories
{
    public interface ISpecialRepository : IRepository<Special>, IArchivableRepository<Special>
    {
        Task<List<Special>> GetCurrentSpecials();
        Task<List<Special>> GetAllWithFoods();
        Task<Special> GetWithFood(int id);
        Task<List<Special>> GetAllForIndex();
        Task<List<Special>> GetAllForAdminIndex();
    }
}
