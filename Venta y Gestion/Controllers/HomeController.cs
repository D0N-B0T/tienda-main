using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Venta_y_Gestion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Retornar la vista de inicio
            return View();
        }

        public ActionResult About()
        {
            // Retornar la vista de acerca de
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            // Retornar la vista de contacto y enviar un mensaje de contacto
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}