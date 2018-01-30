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
    public class SpecialController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ISpecialRepository specialRepository;
        private IFoodRepository foodRepository;

        private RestaurantContext restaurantContext;

        public SpecialController()
        {
            restaurantContext = new RestaurantContext();

            specialRepository = new SpecialRepository(restaurantContext);
            foodRepository = new FoodRepository(restaurantContext);
            _unitOfWork = new UnitOfWork(restaurantContext);
        }

        public ActionResult Index() {
            var specials = specialRepository.GetAll();

            return View(new SpecialListViewModel { Specials = specials });
        }

        public ActionResult Edit(int id) {
            var specialInDb = specialRepository.GetWithFood(id);

            if (specialInDb == null) {
                return HttpNotFound();
            } 

            return View("SpecialForm", new SpecialFormViewModel(specialInDb));
        }

        public ActionResult New() {
            return View("SpecialForm", new SpecialFormViewModel());
        }


        //use an optionnal parameter
        public ActionResult Save(SpecialFormViewModel specialVM) {
            Special specialInDb = specialRepository.GetWithFood(specialVM.Id);

            if (specialInDb == null)
            {
                specialRepository.Add(ConvertSpecialFormViewModelToSpecial(specialVM));

            }
            else {
                specialInDb.Name = specialVM.Name;
                specialInDb.Description = specialVM.Description;
                specialInDb.StartDate = specialVM.StartDate;
                specialInDb.EndDate = specialVM.EndDate;
                //specialInDb.FoodsOnSpecial = specialVM.FoodsOnSpecial;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult PickFood(int specialId) {
            var specialInDb = specialRepository.Get(specialId);
            
            //what if we add the same food?
            var foods = foodRepository.GetAll();

            return View("PickFood", new PickFoodViewModel(specialInDb.Id, foods));

        }

        public ActionResult AddFoodToSpecial(int specialId, int foodId) {
            var specialInDb = specialRepository.GetWithFood(specialId);

            var foodInDb = foodRepository.Get(foodId);

            specialInDb.FoodsOnSpecial.Add(foodInDb);

            _unitOfWork.Complete();

            return View("SpecialForm", new SpecialFormViewModel(specialInDb));
        }

        private Special ConvertSpecialFormViewModelToSpecial(SpecialFormViewModel specialVM)
        {
            Special special = new Special
            {
                Name = specialVM.Name,
                Description = specialVM.Description,
                StartDate = specialVM.StartDate,
                EndDate = specialVM.EndDate,
                //FoodsOnSpecial = specialVM.FoodsOnSpecial
            };

            return special;
        }
    }
}