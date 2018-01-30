using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Dtos
{
    public class ExtraDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AddedPrice { get; set; }
    }
}