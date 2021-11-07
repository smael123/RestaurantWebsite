using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using RestaurantWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> New(int foodId) {
            Food food = await _unitOfWork.Foods.SingleOrDefault(c => c.Id == foodId);
            //you only need two things, make a repository method that only gets those things

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(food);

            return View("FoodPictureForm", viewModel);

        }

        public async Task<ActionResult> Edit(int id) {
            //i believe you've heard of navigational properties...
            FoodPicture foodPicture = await _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == id);
            Food food = await _unitOfWork.Foods.SingleOrDefault(c => c.Id == foodPicture.FoodId);

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(foodPicture, food);

            return View("FoodPictureForm", viewModel);
        }

        public async Task<ActionResult> Save(FoodPictureFormViewModel viewModel) {
            string relativeFolderPath = "/Images/Food/";

            string newFileName = viewModel.FoodName + Path.GetExtension(viewModel.File.FileName);
            string relativeFilePath = relativeFolderPath + newFileName;
            string absoluteFilePath = HttpContext.Server.MapPath("~" + relativeFolderPath) + newFileName;

            FoodPicture foodPictureInDb = await _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == viewModel.Id);

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

            await _unitOfWork.Complete();

            return RedirectToAction("Edit", "Food", new { Id = viewModel.FoodId });
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult GetAllOfFood(int foodId) {
            List<FoodPicture> foodPictures = _unitOfWork.FoodPictures.GetFoodPicturesOfFood(foodId).Result;

            FoodPicturePreviewListViewModel foodPicturePreviewListViewModel = new FoodPicturePreviewListViewModel(foodPictures, foodId);

            return PartialView("_FoodPicturePreview", foodPicturePreviewListViewModel);
        }

        //partial view
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult MenuPreview(int foodId) {
            FoodPicture foodPicture = _unitOfWork.FoodPictures.GetFirstPictureOfFood(foodId).Result; 

            return PartialView("_FoodPictureMenuPreview", foodPicture);
        }

        public async Task<ActionResult> Delete(int id, int foodId = 0) {
            //string relativeFolderPath = "/Images/Food/";

            FoodPicture foodPictureInDb = await _unitOfWork.FoodPictures.SingleOrDefault(c => c.Id == id);

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

            await _unitOfWork.Complete();


            return (foodId == 0) ? RedirectToAction("Index", "Food") : RedirectToAction("Edit", "Food", new { id = foodId });
        }
    }
}