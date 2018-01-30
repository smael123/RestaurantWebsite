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
        private IFoodRepository foodRepository;
        private IExtraRepository extraRepository;

        private RestaurantContext restaurantContext;

        public FoodController() {
            restaurantContext = new RestaurantContext();

            foodRepository = new FoodRepository(restaurantContext);
            extraRepository = new ExtraRepository(restaurantContext);
            _unitOfWork = new UnitOfWork(restaurantContext);
        }

        // GET: Food
        public ActionResult Index()
        {
            var foods = foodRepository.GetAll();

            return View("Menu", new FoodListViewModel { Foods = foods });
        }

        public ActionResult New() {
            return View("FoodForm", new FoodFormViewModel());
        }

        public ActionResult Edit(int id) {
            var foodInDb = foodRepository.GetWithExtra(id);

            if (foodInDb == null) {
                return HttpNotFound();
            }

            //IEnumerable<Extra> extras = extraRepository.GetExtrasOfFood(foodInDb.Id);

            //foodInDb.Extras = new Collection<Extra>(extras);

            //foodInDb.Extras = extras.Select(e => new Extra
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    AddedPrice = e.AddedPrice
            //});

            //return View("FoodForm", new Food
            //{
            //    Id = foodInDb.Id,
            //    Name = foodInDb.Name,
            //    Description = foodInDb.Description,
            //    BasePrice = foodInDb.BasePrice,
            //    Extras = foodInDb.Extras
            //});

            return View("FoodForm", new FoodFormViewModel(foodInDb));
        }

        public ActionResult Save(FoodFormViewModel foodVM) {
            Food foodInDb = foodRepository.GetWithExtra(foodVM.Id);

            if (foodInDb == null)
            {
                foodRepository.Add(ConvertFoodFormViewModelToFood(foodVM));
            }
            else {
                foodInDb.Name = foodVM.Name;
                foodInDb.Description = foodVM.Description;
                foodInDb.BasePrice = foodVM.BasePrice;
                //foodInDb.Extras = food.Extras;
            }


            //if (food == null)
            //{
            //    food = new Food
            //    {
            //        Name = foodVM.Name,
            //        Description = foodVM.Description,
            //        BasePrice = foodVM.BasePrice,
            //        Extras = foodVM.Extras
            //    };

            //    //extras = new Enumerable<Extra>(foodVM.Extras);

            //    foodRepository.Add(food);
            //    //extraRepository.AddRange(foodVM.Extras);
            //}
            //else {
            //    food.Name = foodVM.Name;
            //    food.Description = foodVM.Description;
            //    food.BasePrice = foodVM.BasePrice;
            //    food.Extras = foodVM.Extras;

            //    //IEnumerable<Extra> extras = extraRepository.GetExtrasOfFood(food.Id);
            //}
            
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