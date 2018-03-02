using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using RestaurantWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWebsite.Controllers
{
    public class FoodPictureController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public FoodPictureController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult New(int foodId) {
            Food food = _unitOfWork.Foods.SingleOrDefault(c => c.Id == foodId);
            //you only need two things, make a repository method that only gets those things

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(food);

            return View("FoodPictureForm", viewModel);

        }

        public ActionResult Edit(int id) {
            FoodPicture foodPicture = _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == id);
            Food food = _unitOfWork.Foods.SingleOrDefault(c => c.Id == foodPicture.FoodId);

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(foodPicture, food);

            return View("FoodPictureForm", viewModel);
        }

        public ActionResult Save(FoodPictureFormViewModel viewModel) {
            string relativeFolderPath = "/Images/Food/";

            string newFileName = viewModel.FoodName + Path.GetExtension(viewModel.File.FileName);
            string relativeFilePath = relativeFolderPath + newFileName;
            string absoluteFilePath = HttpContext.Server.MapPath("~" + relativeFolderPath) + newFileName;

            FoodPicture foodPictureInDb = _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == viewModel.Id);

            if (foodPictureInDb == null)
            {
                _unitOfWork.FoodPictures.Add(new FoodPicture
                {
                    FilePath = relativeFilePath,
                    FoodId = viewModel.FoodId
                });
            }
            else {
                foodPictureInDb.FilePath = relativeFilePath;
            }

            viewModel.File.SaveAs(absoluteFilePath);

            _unitOfWork.Complete();

            return RedirectToAction("Edit", "Food", new { Id = viewModel.FoodId });
        }

        [AllowAnonymous]
        public ActionResult GetAllOfFood(int foodId) {
            List<FoodPicture> foodPictures = (List<FoodPicture>)_unitOfWork.FoodPictures.GetFoodPicturesOfFood(foodId);

            FoodPicturePreviewListViewModel foodPicturePreviewListViewModel = new FoodPicturePreviewListViewModel(foodPictures, foodId);

            return View("_FoodPicturePreview", foodPicturePreviewListViewModel);
        }

        [AllowAnonymous]
        public ActionResult MenuPreview(int foodId) {
            FoodPicture foodPicture = _unitOfWork.FoodPictures.GetFirstPictureOfFood(foodId); 

            return View("_FoodPictureMenuPreview", foodPicture);
        }

        public ActionResult Delete(int id, int foodId = 0) {
            //string relativeFolderPath = "/Images/Food/";

            FoodPicture foodPictureInDb = _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == id);

            if (foodPictureInDb == null) {
                return HttpNotFound();
            }

            _unitOfWork.FoodPictures.Remove(foodPictureInDb);

            string absoluteFilePath = HttpContext.Server.MapPath("~") + foodPictureInDb.FilePath;

            try
            {
                System.IO.File.Delete(absoluteFilePath);
            }
            catch (UnauthorizedAccessException)
            {
                //basically I want to return the Edit page with the error. I will have to have an optional argument for a error string in the controller
            }

            _unitOfWork.Complete();


            return (foodId == 0) ? RedirectToAction("Index", "Food") : RedirectToAction("Edit", "Food", new { id = foodId });
        }
    }
}