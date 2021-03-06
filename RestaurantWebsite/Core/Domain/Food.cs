﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.Core.Domain
{
    public class Food : IArchivable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public virtual ICollection<Extra> Extras { get; set; }

        public virtual ICollection<Special> SpecialsItBelongsTo { get; set; }
        public virtual ICollection<FoodPicture> FoodPictures { get; set; }
        public bool IsArchived { get; set; }
    }
}