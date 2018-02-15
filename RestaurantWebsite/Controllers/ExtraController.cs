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
    public class ExtraController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ExtraController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult New(int foodId) {
            return View("_ExtraForm", new ExtraFormViewModel(foodId));
        }

        public ActionResult Edit(int id) {
            var extraInDb = _unitOfWork.Extras.SingleOrDefault(c => c.Id == id);

            if (extraInDb == null) {
                return HttpNotFound();
            }

            return View("_ExtraForm", new ExtraFormViewModel(extraInDb));
        }

        public ActionResult Save(ExtraFormViewModel extraVM) {
            Extra extraInDb = _unitOfWork.Extras.SingleOrDefault(c => c.Id == extraVM.Id);

            if (extraInDb == null)
            {
                _unitOfWork.Extras.Add(ConvertExtraFormViewModelToExtra(extraVM));
            }
            else
            {
                extraInDb.Name = extraVM.Name;
                extraInDb.AddedPrice = extraVM.AddedPrice;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Edit", "Food", new {Id = extraVM.FoodId});
        }

        //id is not needed since this should only be used by the method if they are making a new food
        private Extra ConvertExtraFormViewModelToExtra(ExtraFormViewModel extraVM)
        {
            Extra extra = new Extra
            {
                Name = extraVM.Name,
                AddedPrice = extraVM.AddedPrice,
                FoodId = extraVM.FoodId
            };

            return extra;
        }
    }


}