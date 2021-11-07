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
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index() {
            var specials = await (User.Identity.IsAuthenticated ? _unitOfWork.Specials.GetAllForAdminIndex() : _unitOfWork.Specials.GetAllForIndex());

            return View(new SpecialListViewModel { Specials = specials });
        }

        public async Task<ActionResult> Edit(int id) {
            var specialInDb = await _unitOfWork.Specials.GetWithFood(id);

            if (specialInDb == null) {
                return HttpNotFound();
            } 

            return View(new SpecialFormViewModel(specialInDb));
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int id) {
            var specialInDb = await _unitOfWork.Specials.GetWithFood(id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            return View(new SpecialFormViewModel(specialInDb));
        }

        public ActionResult New() {
            return View("SpecialForm", new SpecialFormViewModel());
        }

        public async Task<ActionResult> Archive(int id) {
            var specialInDb = await _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null) {
                return HttpNotFound();
            }

            specialInDb.IsArchived = true;

            await _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Restore(int id)
        {
            var specialInDb = await _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            specialInDb.IsArchived = false;

            await _unitOfWork.Complete();

            return RedirectToAction("Index");
        }


        //use an optional parameter
        public async Task<ActionResult> Save(SpecialFormViewModel specialVM) {
            Special specialInDb = await _unitOfWork.Specials.GetWithFood(specialVM.Id);

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

            await _unitOfWork.Complete();

            return RedirectToAction("AdminIndex");
        }

        public async Task<ActionResult> PickFood(int specialId) {
            var specialInDb = _unitOfWork.Specials.Get(specialId);
            
            //what if we add the same food?
            var foods = await _unitOfWork.Foods.GetAll();

            return View("PickFood", new PickFoodViewModel(specialInDb.Id, foods));

        }

        public async Task<ActionResult> AddFoodToSpecial(int specialId, int foodId) {
            var specialInDb = await _unitOfWork.Specials.GetWithFood(specialId);

            var foodInDb = await _unitOfWork.Foods.Get(foodId);

            specialInDb.FoodsOnSpecial.Add(foodInDb);

            await _unitOfWork.Complete();

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
        [ChildActionOnly]
        public ActionResult EditPicture(int id) {
            var specialInDb = _unitOfWork.Specials.SingleOrDefault(c=> c.Id == id).Result;

            if (specialInDb == null)
            {
                return HttpNotFound();
            }

            return PartialView("SpecialPictureForm", new SpecialImageFormViewModel(specialInDb));
        }

        public async Task<ActionResult> SavePicture(SpecialImageFormViewModel viewModel) {
            string relativeFilePath = relativeFolderPath + viewModel.SpecialName + Path.GetExtension(viewModel.File.FileName);
            string absoluteFilePath = HttpContext.Server.MapPath("~" + relativeFolderPath) + viewModel.SpecialName + Path.GetExtension(viewModel.File.FileName);

            //RestaurantPicture restaurantPictureInDb = restaurantPictureRepository.SingleOrDefault(c => c.Id == viewModel.PictureId);
            Special specialInDb = await _unitOfWork.Specials.Get(viewModel.SpecialId);

            specialInDb.PictureFilePath = relativeFilePath;

            //save the picture as a file
            viewModel.File.SaveAs(absoluteFilePath);

            await _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeletePicture(int id) {
            Special specialInDb = await _unitOfWork.Specials.SingleOrDefault(c => c.Id == id);

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

            await _unitOfWork.Complete();

            return RedirectToAction("Edit", new { id });
        }
    }
}