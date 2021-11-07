using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using RestaurantWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var foods = await (User.Identity.IsAuthenticated ? _unitOfWork.Foods.GetAllForAdminIndex() : _unitOfWork.Foods.GetAllForIndex());

            return View(new FoodListViewModel { Foods = foods });
        }

        public ActionResult New() {
            return View("Edit", new FoodFormViewModel());
        }

        public async Task<ActionResult> Edit(int id) {
            var foodInDb = await _unitOfWork.Foods.GetFoodForEdit(id);

            if (foodInDb == null) {
                return HttpNotFound();
            }

            return View(new FoodFormViewModel(foodInDb));
        }

        public async Task<ActionResult> Archive(int id)
        {
            var foodInDb = await _unitOfWork.Foods.SingleOrDefault(c => c.Id == id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            foodInDb.IsArchived = true;

            await _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Restore(int id) {
            var foodInDb = await _unitOfWork.Foods.SingleOrDefault(c => c.Id == id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            foodInDb.IsArchived = false;

            await _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var foodInDb = await _unitOfWork.Foods.GetFoodForDetails(id);

            if (foodInDb == null)
            {
                return HttpNotFound();
            }

            return View(new FoodFormViewModel(foodInDb));
        }

        //can only save the Food not extras!
        public async Task<ActionResult> Save(FoodFormViewModel foodVM) {
            Food foodInDb = await _unitOfWork.Foods.GetWithExtra(foodVM.Id);

            if (foodInDb == null)
            {
                _unitOfWork.Foods.Add(ConvertFoodFormViewModelToFood(foodVM));
            }
            else {
                foodInDb.Name = foodVM.Name;
                foodInDb.Description = foodVM.Description;
                foodInDb.BasePrice = foodVM.BasePrice;
            }
            
            await _unitOfWork.Complete();

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