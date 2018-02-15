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
        private FoodPictureRepository foodPictureRepository;
        private FoodRepository foodRepository;

        private RestaurantContext restaurantContext;

        public FoodPictureController()
        {
            restaurantContext = new RestaurantContext();

            foodRepository = new FoodRepository(restaurantContext);
            foodPictureRepository = new FoodPictureRepository(restaurantContext);
            _unitOfWork = new UnitOfWork(restaurantContext);
        }

        public ActionResult New(int foodId) {
            Food food = foodRepository.SingleOrDefault(c => c.Id == foodId);
            //you only need two things, make a repository method that only gets those things

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(food);

            return View("FoodPictureForm", viewModel);

        }

        public ActionResult Edit(int id) {
            FoodPicture foodPicture = foodPictureRepository.SingleOrDefault(c => c.Id == id);
            Food food = foodRepository.SingleOrDefault(c => c.Id == foodPicture.FoodId);

            FoodPictureFormViewModel viewModel = new FoodPictureFormViewModel(foodPicture, food);

            return View("FoodPictureForm", viewModel);
        }

        public ActionResult Save(FoodPictureFormViewModel viewModel) {
            string relativeFolderPath = "/Images/Food/";

            string newFileName = viewModel.FoodName + Path.GetExtension(viewModel.File.FileName);
            string relativeFilePath = relativeFolderPath + newFileName;
            string absoluteFilePath = HttpContext.Server.MapPath("~" + relativeFolderPath) + newFileName;

            FoodPicture foodPictureInDb = foodPictureRepository.SingleOrDefault(c => c.Id == viewModel.Id);

            if (foodPictureInDb == null)
            {
                foodPictureRepository.Add(new FoodPicture
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
            List<FoodPicture> foodPictures = (List<FoodPicture>)foodPictureRepository.GetFoodPicturesOfFood(foodId);

            FoodPicturePreviewListViewModel foodPicturePreviewListViewModel = new FoodPicturePreviewListViewModel(foodPictures, foodId);

            return View("_FoodPicturePreview", foodPicturePreviewListViewModel);
        }

        [AllowAnonymous]
        public ActionResult MenuPreview(int foodId) {
            FoodPicture foodPicture = foodPictureRepository.GetFirstPictureOfFood(foodId); 

            return View("_FoodPictureMenuPreview", foodPicture);
        }
    }
}