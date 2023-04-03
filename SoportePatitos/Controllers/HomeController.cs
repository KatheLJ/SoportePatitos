using SoportePatitosBD.Modelo;
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
        public ActionResult Inicio()
        {
            return View();
        }


        //Accion que muestra el inicio de la aplicacion (Al iniciar sesiòn)
        public ActionResult Index()
        {
            return View();
        }

        //Accion que muestra informacion acerca de SoportePatitos
        public ActionResult About()
        {
            
            return View();
        }

        //Accion que muestra la pantalla de inicio de sesion al empleado
        public ActionResult Login()
        {
            return View();
        }




        //Accion que valide los datos y permite o deniega el ingreso al usuario
        public ActionResult InicioSesion(Empleado pEmpleado)
        {
            if (ModelState.IsValid)
            {
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    var user = ContextoBD.Empleado.FirstOrDefault(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña));

                    if (user != null)
                    {
                        // Si se encuentra el usuario, se establecen las variables de sesión correspondientes
                        if (user.ID_perfil == 1)
                        {
                            Session["Gerencia"] = user.Usuario;
                        }
                        else if (user.ID_perfil == 2)
                        {
                            Session["RH"] = user.Usuario;
                        }
                        else if (user.ID_perfil == 3)
                        {
                            Session["Empleado"] = user.Usuario;
                        }

                        Session["Cedula"] = user.Cedula;
                        Session["Nombre"] = user.Nombre_Empleado;

                        // Redireccionar al usuario a la página de inicio
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Si no se encuentra el usuario, redireccionar a la página de registro
            return RedirectToAction("Registro_Empleados", "RecursosHumanos");
        }


        public ActionResult CambioContraseña()
        {
            return View();
        }



        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Inicio", "Home");
        }



    }
}