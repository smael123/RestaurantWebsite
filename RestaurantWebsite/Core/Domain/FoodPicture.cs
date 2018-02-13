using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Core.Domain
{
    public class FoodPicture
    {
        public int Id { get; set; }
        public string FilePath { get; set; }

        //nav property
        public int FoodId { get; set; }
    }
}