using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class SpecialImageFormViewModel
    {
        public int SpecialId { get; set; }
        public string SpecialName { get; set; }

        public int PictureId { get; set; }
        public string FilePath { get; set; }


        [Display(Name = "Upload Image:")]
        public HttpPostedFileBase File { get; set; }

        public SpecialImageFormViewModel() { }

        public SpecialImageFormViewModel(Special special) {
            SpecialId = special.Id;
            SpecialName = special.Name;
            FilePath = special.PictureFilePath;
        }
    }
}