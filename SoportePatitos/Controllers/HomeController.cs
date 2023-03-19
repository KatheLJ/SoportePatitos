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
                //Se le indica al sistema que utilice dicha entidad para conectarse a la BD
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    var data = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña) && a.ID_perfil.Equals(1)).ToList();

                    var data2 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña) && a.ID_perfil.Equals(2)).ToList();

                    var data3 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña) && a.ID_perfil.Equals(3)).ToList();

                    //var data = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    //a.Contraseña.Equals(pEmpleado.Contraseña) && a.Perfil.Equals(pEmpleado.Perfil)).ToList();

                    /* var data2 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                     a.Contraseña.Equals(pEmpleado.Contraseña)).ToList();

                     var data3 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña)).ToList();*/


                    Session["Gerencia"] = null;
                    Session["RH"] = null;
                    Session["Empleado"] = null;


                    //Permite el manejo de las sesiones y los perfiles del sistema 

                    if (data.Count() > 0 && pEmpleado.ID_perfil.Equals(1))
                    {
                        Session["Gerencia"] = data.FirstOrDefault().Usuario;

                        //Para los datos del perfil del tipo perfil 1
                        Session["Cedula"] = data.FirstOrDefault().Cedula;
                        Session["Nombre"] = data.FirstOrDefault().Nombre_Empleado;


                        //Cookie que permite manejar el usuario en sesión, en lugar de usar Session
                        // FormsAuthentication.SetAuthCookie(pEmpleado.Usuario, false);
                        return RedirectToAction("Index", "Home");

                    }
                    else if (data2.Count() > 0 && pEmpleado.ID_perfil.Equals(2))
                    {

                        Session["RH"] = data2.FirstOrDefault().Usuario;

                        //Para los datos del perfil del tipo perfil 2
                        Session["Cedula"] = data2.FirstOrDefault().Cedula;
                        Session["Nombre"] = data2.FirstOrDefault().Nombre_Empleado;


                        return RedirectToAction("Index", "Home");
                    }
                    else if (data3.Count() > 0 && pEmpleado.ID_perfil.Equals(3))
                    {

                        Session["Empleado"] = data3.FirstOrDefault().Usuario;

                        //Para los datos del perfil del tipo perfil 3
                        Session["Cedula"] = data3.FirstOrDefault().Cedula;
                        Session["Nombre"] = data3.FirstOrDefault().Nombre_Empleado;


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        return RedirectToAction("Registro_Empleados", "RecursosHumanos");
                    }


                }
            }
            return View();
        }






    }
}