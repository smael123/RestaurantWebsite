using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class PickFoodViewModel
    {
        public IEnumerable<Food> Foods { get; set; }

        public int SpecialId { get; set; }

        public PickFoodViewModel() { }
        public PickFoodViewModel(int specialId, IEnumerable<Food> foods) {
            SpecialId = specialId;

            Foods = foods;
        }
    }

    
}