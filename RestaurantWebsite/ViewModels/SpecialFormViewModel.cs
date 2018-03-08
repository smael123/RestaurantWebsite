using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class SpecialFormViewModel
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsArchived { get; set; }

        public DateTime EndDate { get; set; }
        public ICollection<Food> FoodsOnSpecial { get; set; }
        public string SpecialPictureFilePath { get; set; }

        public SpecialFormViewModel() {
            FoodsOnSpecial = new List<Food>();
        }

        public SpecialFormViewModel(Special special) {
            Id = special.Id;
            Name = special.Name;
            Description = special.Description;
            StartDate = special.StartDate;
            EndDate = special.EndDate;
            IsArchived = special.IsArchived;
            FoodsOnSpecial = special.FoodsOnSpecial;
            SpecialPictureFilePath = special.PictureFilePath;
        }
    }
}