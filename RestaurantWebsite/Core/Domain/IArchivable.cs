using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebsite.Core.Domain
{
    public interface IArchivable
    {
         bool IsArchived { get; set; }
    }
}
