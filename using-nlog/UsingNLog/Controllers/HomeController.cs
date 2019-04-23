using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace UsingNLog.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            try
            {
                object sayi = 0;
                int deger = 5 / (int)sayi;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return View();
        }
    }
}