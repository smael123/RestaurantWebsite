using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Core.Domain
{
    public class Extra
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AddedPrice { get; set; }

        //navigational properties
        public int FoodId { get; set; }
        public virtual Food Foods { get; set; }
    }
}