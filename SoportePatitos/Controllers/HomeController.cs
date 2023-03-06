using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoportePatitos.Controllers
{
    public class HomeController : Controller
    {
        //Accion que muestra el inicio de la aplicacion
        public ActionResult Index()
        {
            return View();
        }

        //Accion que muestra informacion acerca de SoportePatitos
        public ActionResult About()
        {
            
            return View();
        }


        


    }
}