using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitPoker.MVC.Controllers
{
    public class InvestController : Controller
    {
        private readonly String _wifStr;

        public InvestController()
        {
            _wifStr = System.Configuration.ConfigurationManager.AppSettings["HDKey"];
        }

        public InvestController(String wifStr)
        {
            _wifStr = wifStr;
        }

        // GET: Invest
        public ActionResult Index()
        {
            Models.Order order = new Models.Order();
            return View(order);
        }

        [HttpPost]
        public ActionResult Buy(Models.Order order)
        {
            ExtPubKey key = ExtPubKey.Parse(_wifStr);
            uint orderID = 1001;
            BitcoinAddress address = key.Derive(orderID).PubKey.GetAddress(Network.Main);

            return View();
        }
    }
}