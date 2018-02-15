using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
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
    public class SpecialController : Controller
    {
        private IUnitOfWork _unitOfWork;

        private static readonly string relativeFolderPath = "/Images/SpecialImages/";

        public SpecialController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [AllowAnonymous]
        public ActionResult Index() {
            var specials = _unitOfWork.Specials.GetAll();

            return View(new SpecialListViewModel { Specials = specials });
        }

        public ActionResult Edit(int id) {
            var specialInDb = _unitOfWork.Specials.GetWithFood(id);

            if (specialInDb == null) {
                return HttpNotFound();
            } 

            return View("SpecialForm", new SpecialFormViewModel(specialInDb));
        }

        public ActionResult New() {
            return View("SpecialForm", new SpecialFormViewModel());
        }


        //use an optional parameter
        public ActionResult Save(SpecialFormViewModel specialVM) {
            Special specialInDb = _unitOfWork.Specials.GetWithFood(specialVM.Id);

            if (specialInDb == null)
            {
                _unitOfWork.Specials.Add(ConvertSpecialFormViewModelToSpecial(specialVM));

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
            var specialInDb = _unitOfWork.Specials.Get(specialId);
            
            //what if we add the same food?
            var foods = _unitOfWork.Foods.GetAll();

            return View("PickFood", new PickFoodViewModel(specialInDb.Id, foods));

        }

        public ActionResult AddFoodToSpecial(int specialId, int foodId) {
            var specialInDb = _unitOfWork.Specials.GetWithFood(specialId);

            var foodInDb = _unitOfWork.Foods.Get(foodId);

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

        //Perhaps picture related controllers should be moved to a separate class
        public ActionResult EditPicture(int id) {
            var specialInDb = _unitOfWork.Specials.SingleOrDefault(c=> c.Id == id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            return View("SpecialPictureForm", new SpecialImageFormViewModel(specialInDb));
        }

        public ActionResult SavePicture(SpecialImageFormViewModel viewModel) {
            string relativeFilePath = relativeFolderPath + viewModel.SpecialName + Path.GetExtension(viewModel.File.FileName);
            string absoluteFilePath = HttpContext.Server.MapPath("~" + relativeFolderPath) + viewModel.SpecialName + Path.GetExtension(viewModel.File.FileName);

            //RestaurantPicture restaurantPictureInDb = restaurantPictureRepository.SingleOrDefault(c => c.Id == viewModel.PictureId);
            Special specialInDb = _unitOfWork.Specials.Get(viewModel.SpecialId);

            specialInDb.PictureFilePath = relativeFilePath;

            //save the picture as a file
            viewModel.File.SaveAs(absoluteFilePath);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}