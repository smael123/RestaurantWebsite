using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Core.Domain
{
    public class Special
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<Food> FoodsOnSpecial { get; set; }
        public string PictureFilePath { get; set; }
        //public int? SpecialPictureId { get; set; }
    }
}