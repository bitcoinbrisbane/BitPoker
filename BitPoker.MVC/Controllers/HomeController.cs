using System.Web.Mvc;

namespace BitPoker.MVC.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}