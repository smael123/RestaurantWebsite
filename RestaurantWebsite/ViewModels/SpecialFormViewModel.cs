using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class SpecialFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Food> FoodsOnSpecial { get; set; }

        public SpecialFormViewModel() {
            FoodsOnSpecial = new List<Food>();
        }

        public SpecialFormViewModel(Special special) {
            Id = special.Id;
            Name = special.Name;
            Description = special.Description;
            StartDate = special.StartDate;
            EndDate = special.EndDate;
            FoodsOnSpecial = special.FoodsOnSpecial;
        }
    }
}