using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class FoodPicturePreviewListViewModel
    {
        public List<FoodPicture> FoodPreviews { get; set; }

        public int FoodId { get; set; }

        public FoodPicturePreviewListViewModel() { }

        public FoodPicturePreviewListViewModel(List<FoodPicture> foodPictures, int foodId) {
            FoodPreviews = new List<FoodPicture>();

            foodPictures.ForEach(foodPicture =>
            {
                FoodPreviews.Add(new FoodPicture
                {
                    Id = foodPicture.Id,
                    FilePath = foodPicture.FilePath
                });
            });

            FoodId = foodId;

            var enumerator = FoodPreviews.GetEnumerator();
        }
    }
}