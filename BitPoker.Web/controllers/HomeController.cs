using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BitPoker.Web.controllers
{
    public class HomeController : Controller
    {

        // GET: Messages
        public ActionResult Index()
        {

            GetMessages();
            return View();
        }

        public void GetMessages()
        {
           
        }

    }
}