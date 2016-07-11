using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitPoker.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy()
        {
            Models.BuyRequest request = new Models.BuyRequest()
            {
                AssetId = "Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ",
                Amount = 1000,
                Rate = 0.002M
            };
            return View(request);
        }

        [HttpPost]
        public ActionResult Buy(Models.BuyRequest request)
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