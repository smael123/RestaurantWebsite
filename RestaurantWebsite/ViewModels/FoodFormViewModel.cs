using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class FoodFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }

        public ICollection<Extra> Extras { get; set; }

        public FoodFormViewModel() {
            Extras = new List<Extra>();
        }

        public FoodFormViewModel(Food food) {
            Id = food.Id;
            Name = food.Name;
            Description = food.Description;
            BasePrice = food.BasePrice;

            Extras = food.Extras;
        }
    }
}