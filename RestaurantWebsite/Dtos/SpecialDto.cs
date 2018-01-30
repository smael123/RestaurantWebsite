using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Dtos
{
    public class SpecialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<FoodDto> FoodsOnSpecial { get; set; }
    }
}