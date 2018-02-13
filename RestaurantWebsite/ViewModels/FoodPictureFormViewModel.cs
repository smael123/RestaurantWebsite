using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class FoodPictureFormViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }

        public int FoodId { get; set; }
        public string FoodName { get; set; }

        [Display(Name = "Upload Image:")]
        public HttpPostedFileBase File { get; set; }

        public FoodPictureFormViewModel() { }

        public FoodPictureFormViewModel(Food food) {
            FoodId = food.Id;
            FoodName = food.Name;
        }
        public FoodPictureFormViewModel(FoodPicture foodPicture, Food food) {
            Id = foodPicture.Id;
            FilePath = foodPicture.FilePath;

            FoodId = food.Id;
            FoodName = food.Name;
        }
    }
}