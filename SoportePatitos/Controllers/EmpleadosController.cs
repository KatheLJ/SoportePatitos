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
using System.Threading;


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
        //Accion que muestra la pantalla para registro el ingreso y la salida del empleado
        public ActionResult MarcaAsistencia()
        {
            //Se inicializan las listas que se usaran más adelante
            List<Models.ViewModels.Estado> estadolst = null;
            int n = 0;

            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los departamentos almacenados en la base de datos
                


                estadolst =
                (from d in ContextoBD.Estado
                 select new Models.ViewModels.Estado
                 {
                     ID_Estado = d.ID_Estado,
                     Descripcion = d.Descripcion
                 }).ToList();

                
                List<SelectListItem> estados = estadolst.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_Estado.ToString(),
                        Selected = false
                    };

                });

               // Asistencia obj = estadolst.Where(x => x.ID_Estado == 1).First();

                ViewData["Estado"] = estados;

                // Obtener la cedula del usuario que ha iniciado sesión
                string cedula = Session["Cedula"].ToString();
                ViewData["Cedula"] = cedula;


                return View();
            }
            
        }



        //Accion que muestra la pantalla para enviar el ingreso/salida (Marcas)
        public ActionResult EnviarAsistencia(Asistencia pAsistencia)
        {
            bool hayMarca = _oGestorAsistencia.VerificarMarca(pAsistencia.Cedula, pAsistencia.Fecha);
            int tipo = hayMarca ? 2 : 1;
            pAsistencia.Tipo = tipo;

            int marcasRegistradas = _oGestorAsistencia.ContarMarcas(pAsistencia.Cedula, pAsistencia.Fecha);

            if (marcasRegistradas >= 2)
            {
                TempData["MensajeError"] = "No se puede agregar una tercera marca para esta fecha y cédula.";
                return RedirectToAction("MarcaAsistencia");
            }
            else
            {
                int registros = _oGestorAsistencia.CrearAsistencia(pAsistencia);
                return RedirectToAction("MarcaAsistencia");
            }
        }




        public ActionResult ValidarAusencias(Asistencia pAsistencia)
        {
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {

                var data = ContextoBD.Asistencia.Where(a => a.Fecha.Equals(DateTime.UtcNow)) ;

                /* if (data = DateTime.Now.ToString())
                 {
                    
                     //En bd buscar por cedulas marcas registradas en un día y si Count <2 AND Count >1 = NO marca entrada o salida
                     //Si Count = 0 = Ausencia
                     //Realizar la revision de a las 11PM de cada noche  y asignar las ausencias

                 }

                 else
                 {

                 }*/
            }
            return View();
        }



        //Accion que muestra el listado de asistencias
        public ActionResult CambioContraseña(int Cedula)
        {
            return View();
        }




    }
}