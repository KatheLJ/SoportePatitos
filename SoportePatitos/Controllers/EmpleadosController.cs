using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoportePatitosBD.Modelo;
using SoportePatitosBD.Interface;
using SoportePatitosBD.Repositorios;
using System.ComponentModel.Design;
using Microsoft.Ajax.Utilities;
using System.Web.Security;

namespace SoportePatitos.Controllers
{
    public class EmpleadosController : Controller
    {

        private readonly IGestorEmpleado _oGestorEmpleado;
        private readonly IGestorEvaluacion _oGestorEvaluacion;
        private readonly IGestorAsistencia _oGestorAsistencia;
        private readonly IGestorPlanilla _oGestorPlanilla;


        //Constructor de el empleado
        public EmpleadosController()
        {
            _oGestorEmpleado = new GestorEmpleado();
            _oGestorEvaluacion = new GestorEvaluacion();
            _oGestorAsistencia = new GestorAsistencia();
            //  _oGestorPlanilla = new GestorPlanilla();
        }



        // GET: Empleado
        //Accion que permite mostrar el formulario para que el empleado se registre
        public ActionResult Registro_Empleados()
        {

            //Se inicializan las listas que se usaran más adelante
            List<Models.ViewModels.Departamento> lst = null;
            List<Models.ViewModels.Puesto> lst2 = null;
            List<Models.ViewModels.Perfil> lst3 = null;
            List<Models.ViewModels.Horario> lst4 = null;

            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los departamentos almacenados en la base de datos
                lst =
                (from d in ContextoBD.Departamento
                 select new Models.ViewModels.Departamento
                 {
                     ID_departamento = d.ID_departamento,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> items = lst.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_departamento.ToString(),
                        Selected = false
                    };

                });
                ViewBag.Items = items;




                //Permite mostrar un dropdownlist con los puestos almacenados en la base de datos
                /*lst2 =
                (from d in ContextoBD.Puesto
                 select new Models.ViewModels.Puesto
                 {
                     ID_puesto = d.ID_puesto,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> items2 = lst2.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_puesto.ToString(),
                        Selected = false
                    };

                });


                //Permite mostrar un dropdownlist con los perfiles almacenados en la base de datos
                lst3 =
                (from d in ContextoBD.Perfil
                 select new Models.ViewModels.Perfil
                 {
                     ID_perfil = d.ID_perfil,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> items3 = lst3.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_perfil.ToString(),
                        Selected = false
                    };

                });

                //Permite mostrar un dropdownlist con los horarios almacenados en la base de datos
                lst4 =
                (from d in ContextoBD.Horario
                 select new Models.ViewModels.Horario
                 {
                     ID_horario = d.ID_horario,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> items4 = lst4.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_horario.ToString(),
                        Selected = false
                    };

                });



                ViewBag.Items = items2;
                ViewBag.Items = items3;
                ViewBag.Items = items4;*/

                return View();
            }


        }




        //Accion que permite Registrar un empleado en la base de datos
        public ActionResult EnviarEmpleado(Empleado pEmpleado)
        {
            int registros = _oGestorEmpleado.CrearEmpleado(pEmpleado);
            return RedirectToAction("Registro_Empleados");
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
                    a.Contraseña.Equals(pEmpleado.Contraseña) && a.Perfil.Equals(pEmpleado.Perfil)).ToList();

                    /* var data2 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                     a.Contraseña.Equals(pEmpleado.Contraseña)).ToList();

                     var data3 = ContextoBD.Empleado.Where(a => a.Usuario.Equals(pEmpleado.Usuario) &&
                    a.Contraseña.Equals(pEmpleado.Contraseña)).ToList();*/


                    Session["Gerencia"] = null;
                    Session["RH"] = null;
                    Session["Empleado"] = null;


                    //Permite el manejo de las sesiones y los perfiles del sistema 

                    if (data.Count() > 0 && pEmpleado.Perfil.Equals(1))
                    {
                        Session["Gerencia"] = data.FirstOrDefault().Usuario;

                        //Para los datos del perfil del tipo perfil 1
                        Session["Cedula"] = data.FirstOrDefault().Cedula;
                        Session["Nombre"] = data.FirstOrDefault().Nombre_Empleado;

                        //Cookie que permite manejar el usuario en sesión, en lugar de usar Session
                        // FormsAuthentication.SetAuthCookie(pEmpleado.Usuario, false);
                        return RedirectToAction("Index", "Home");

                    }
                    else if (data.Count() > 0 && pEmpleado.Perfil.Equals(2))
                     {

                         Session["RH"] = data.FirstOrDefault().Usuario;

                         //Para los datos del perfil del tipo perfil 2
                         Session["Cedula"] = data.FirstOrDefault().Cedula;
                         Session["Nombre"] = data.FirstOrDefault().Nombre_Empleado;


                         return RedirectToAction("Index", "Home");
                     }
                     else if (data.Count() > 0 && pEmpleado.Perfil.Equals(3))
                     {

                         Session["Empleado"] = data.FirstOrDefault().Usuario;

                         //Para los datos del perfil del tipo perfil 3
                         Session["Cedula"] = data.FirstOrDefault().Cedula;
                         Session["Nombre"] = data.FirstOrDefault().Nombre_Empleado;


                         return RedirectToAction("Index", "Home");
                     }
                     else
                     {

                        return RedirectToAction("Registro_Empleados");
                     }


                }
            }
            return View();
        }


        //Accion que muestra la pantalla de para evaluar a un empleado
        public ActionResult Evaluacion()
        {

            return View();
        }

        //[Authorize] 
        //Permite manejar a que páginas esta autorizada el usuario y cuales no
        //Accion envia la evaluación realizada a un empleado
        public ActionResult EnviarEvaluacion(Evaluacion pEvaluacion)
        {
            int registros = _oGestorEvaluacion.CrearEvaluacion(pEvaluacion);
            return RedirectToAction("Evaluacion");
        }


        //Accion que muestra la pantalla con el reporte de la evaluacion
        public ActionResult ListadoEmpleados()
        {
            IEnumerable<Empleado> empleados = _oGestorEmpleado.ListadoEmpleados();
            return View(empleados);
        }


        //Accion que muestra la pantalla con el reporte de la evaluacion
        public ActionResult ReporteEmpleado()
        {
            IEnumerable<Evaluacion> evaluacion = _oGestorEvaluacion.ListadoEvaluacion();
            return View(evaluacion);
        }



        //Accion que muestra la pantalla para registro el ingreso y la salida del empleado
        public ActionResult MarcaAsistencia()
        {
            
            return View();
        }



        //Accion que muestra la pantalla para enviar el ingreso/salida (Marcas)
        public ActionResult EnviarAsistencia(Asistencia pAsistencia)
        {
            int registros = _oGestorAsistencia.CrearAsistencia(pAsistencia);
            return RedirectToAction("MarcaAsistencia");
        }



        //Accion que muestra la pantalla en donde se maneja la planilla
       /* public ActionResult Planilla(Planilla pPlanilla)
        {
            int registros = _oGestorPlanilla.CrearPLanilla(pPlanillla);
            return RedirectToAction("Planilla");
        }*/



    }
}