using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using RestaurantWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWebsite.Controllers
{
    public class FoodController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public FoodController() {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Food
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<Food> foods = (User.Identity.IsAuthenticated) ? _unitOfWork.Foods.GetAllForAdminIndex() : _unitOfWork.Foods.GetAllForIndex();

            //var foods = _unitOfWork.Foods.GetAll();

            return View(new FoodListViewModel { Foods = foods });
        }



        public ActionResult New() {
            return View("Edit", new FoodFormViewModel());
        }

        public ActionResult Edit(int id) {
            var foodInDb = _unitOfWork.Foods.GetFoodForEdit(id);

            if (foodInDb == null) {
                return HttpNotFound();
            }

            return View(new FoodFormViewModel(foodInDb));
        }

        public ActionResult Archive(int id)
        {
            var foodInDb = _unitOfWork.Foods.SingleOrDefault(c => c.Id == id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            foodInDb.IsArchived = true;

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Restore(int id) {
            var foodInDb = _unitOfWork.Foods.SingleOrDefault(c => c.Id == id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            foodInDb.IsArchived = false;

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var foodInDb = _unitOfWork.Foods.GetFoodForDetails(id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            return View(new FoodFormViewModel(foodInDb));
        }

        //can only save the Food not extras!
        public ActionResult Save(FoodFormViewModel foodVM) {
            Food foodInDb = _unitOfWork.Foods.GetWithExtra(foodVM.Id);

            if (foodInDb == null)
            {
                _unitOfWork.Foods.Add(ConvertFoodFormViewModelToFood(foodVM));
            }
            else {
                foodInDb.Name = foodVM.Name;
                foodInDb.Description = foodVM.Description;
                foodInDb.BasePrice = foodVM.BasePrice;
            }
            
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult GetExtraEditor() {
            return PartialView("Shared/_ExtraForm", new Extra());
        }

        //id is not needed since this should only be used by the method if they are making a new food
        private Food ConvertFoodFormViewModelToFood(FoodFormViewModel foodVM) {
            Food food = new Food
            {
                Name = foodVM.Name,
                Description = foodVM.Description,
                BasePrice = foodVM.BasePrice,
                Extras = foodVM.Extras
            };

            return food;
        }
    }
}