using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class ExtraFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AddedPrice { get; set; }

        public int FoodId { get; set; }

        public ExtraFormViewModel() { }

        public ExtraFormViewModel(int foodId) {
            FoodId = foodId;
        }

        public ExtraFormViewModel(Extra extra) {
            Id = extra.Id;
            Name = extra.Name;
            AddedPrice = extra.AddedPrice;

            FoodId = extra.FoodId;
        }
    }


}