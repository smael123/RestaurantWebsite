using RestaurantWebsite.Core;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWebsite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController() {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var specials = _unitOfWork.Specials.GetCurrentSpecials();

            HomeViewModel viewModel = new HomeViewModel {
                CurrentSpecials = specials
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}