using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class FoodFormViewModel
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        //https://stackoverflow.com/q/24188215
        [DataType(DataType.Currency)]
        public decimal BasePrice { get; set; }

        public ICollection<Extra> Extras { get; set; }
        public ICollection<FoodPicture> FoodPictures { get; set; }

        public FoodFormViewModel() {
            Extras = new List<Extra>();
            FoodPictures = new List<FoodPicture>();
        }

        public FoodFormViewModel(Food food) {
            Id = food.Id;
            Name = food.Name;
            Description = food.Description;
            BasePrice = food.BasePrice;

            Extras = food.Extras;
            FoodPictures = food.FoodPictures;
        }
    }
}