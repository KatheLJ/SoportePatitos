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
        //Acción que muestra el inicio de la aplicacion
        public ActionResult Inicio()
        {
            return View();
        }


        //Acción que muestra el inicio de la aplicacion (Al iniciar sesiòn)
        public ActionResult Index()
        {
            return View();
        }

        //Acción que muestra informacion acerca de SoportePatitos
        public ActionResult About()
        {
            
            return View();
        }

        //Acción que muestra la pantalla de inicio de sesion al empleado
        public ActionResult Login()
        {
            return View();
        }




        //Accion que valide los datos y permite o deniega el ingreso al usuario
        public ActionResult InicioSesion(Empleado pEmpleado)
        {
            //Si el esetado del Modelo es válido
            if (ModelState.IsValid)
            {
                //Se llama a una conexión de tipo SoportePatitosEntities
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    //Se crea una variable User, que recibirá los paramétros que de el usuario y los comparará con los que están en la BD
                    var user = ContextoBD.Empleado.FirstOrDefault(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña));

                    //Si está variable es diferente de nulo
                    if (user != null)
                    {
                        // Si se encuentra el usuario, se establecen las variables de sesión correspondientes
                        //Si el perfil es 1
                        if (user.ID_perfil == 1)
                        {
                            //Será un usuario de tipo Gerencia
                            Session["Gerencia"] = user.Usuario;
                        }
                        //Si el perfil es tipo 2
                        else if (user.ID_perfil == 2)
                        {
                            //Será un usuario de Recursos Humanos
                            Session["RH"] = user.Usuario;
                        }
                        //Si el perfil es tipo 3
                        else if (user.ID_perfil == 3)
                        { 
                            //Será un empleado general
                            Session["Empleado"] = user.Usuario;
                        }

                        //Se deben guardar los datos de la cédula y el nombre del empleado
                        Session["Cedula"] = user.Cedula;
                        Session["Nombre"] = user.Nombre_Empleado;

                        // Redireccionar al usuario a la página de inicio, si todo es correcto
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Si no se encuentra el usuario, redireccionar a la página de registro
            return RedirectToAction("Registro_Empleados", "RecursosHumanos");
        }


        //Acción que muestra la pantalla de cambio de contraseña, luego del primer login
        public ActionResult CambioContraseña()
        {
            return View();
        }


        //Acción que permite salir de la sesión al usuario
        public ActionResult Logout()
        {
            //Se abandona la sesión
            Session.Abandon();
            //Se borra todos los datos 
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            //Se regresa al Inicio del sitio
            return RedirectToAction("Inicio", "Home");
        }



    }
}