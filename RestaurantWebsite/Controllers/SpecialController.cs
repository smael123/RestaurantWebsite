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
            var specials = (User.Identity.IsAuthenticated) ? _unitOfWork.Specials.GetAllForAdminIndex() : _unitOfWork.Specials.GetAllForIndex();

            return View(new SpecialListViewModel { Specials = specials });
        }

        //public ActionResult AdminIndex() {
        //    var specials = _unitOfWork.Specials.GetAll();

        //    return View(new SpecialListViewModel { Specials = specials });
        //}

        public ActionResult Edit(int id) {
            var specialInDb = _unitOfWork.Specials.GetWithFood(id);

            if (specialInDb == null) {
                return HttpNotFound();
            } 

            return View(new SpecialFormViewModel(specialInDb));
        }

        [AllowAnonymous]
        public ActionResult Details(int id) {
            var specialInDb = _unitOfWork.Specials.GetWithFood(id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            return View(new SpecialFormViewModel(specialInDb));
        }

        public ActionResult New() {
            return View("SpecialForm", new SpecialFormViewModel());
        }

        public ActionResult Archive(int id) {
            var specialInDb = _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null) {
                return HttpNotFound();
            }

            specialInDb.IsArchived = true;

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Restore(int id)
        {
            var specialInDb = _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            specialInDb.IsArchived = false;

            _unitOfWork.Complete();

            return RedirectToAction("Index");
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

            return RedirectToAction("AdminIndex");
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

        public ActionResult DeletePicture(int id) {
            Special specialInDb = _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            string absoluteFilePath = HttpContext.Server.MapPath("~") + specialInDb.PictureFilePath;

            specialInDb.PictureFilePath = null;

            try
            {
                System.IO.File.Delete(absoluteFilePath);
            }
            catch (UnauthorizedAccessException) {
                //basically I want to return the Edit page with the error. I will have to have an optional argument for a error string in the controller
            }

            _unitOfWork.Complete();

            return RedirectToAction("Edit", new { id });
        }
    }
}