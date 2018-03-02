using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace RestaurantWebsite.ServerManager
{
    public class ImageManager
    {
        public string AbsoluteFilePath { get; set; }
        public HttpPostedFileBase File { get; set; }

        //accepted file types
        //maximum file limit

        public ImageManager(string absoluteFilePath, HttpPostedFileBase file) {
            AbsoluteFilePath = absoluteFilePath;
            File = file;
        }

        public void SaveImage(HttpPostedFileBase file)
        {
            //if (file.ContentLength)

            file.SaveAs(AbsoluteFilePath);
        }
    }
}