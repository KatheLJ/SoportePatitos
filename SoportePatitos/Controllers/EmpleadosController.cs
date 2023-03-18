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
            List<Models.ViewModels.Tipo> lst = null;
            List<Models.ViewModels.Estado> estadolst = null;
            int n = 0;

            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los departamentos almacenados en la base de datos
                lst =
                (from d in ContextoBD.Tipo
                 select new Models.ViewModels.Tipo
                 {
                     ID_tipo = d.ID_tipo,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> tipos = lst.ConvertAll(d =>
                {

                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_tipo.ToString(),
                        Selected = false
                    };

                });

                ViewData["Tipo"] = tipos;


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

                return View();
            }
            
        }



        //Accion que muestra la pantalla para enviar el ingreso/salida (Marcas)
        public ActionResult EnviarAsistencia(Asistencia pAsistencia)
        {
            int registros = _oGestorAsistencia.CrearAsistencia(pAsistencia);
            return RedirectToAction("MarcaAsistencia");
        }


        //Accion que muestra el listado de asistencias
        public ActionResult ListadoAsistencia()
        {
            IEnumerable<Asistencia> asistencias = _oGestorAsistencia.ListadoAsistencia();
            return View(asistencias);
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





        //Accion que muestra la pantalla en donde se maneja la planilla
        /* public ActionResult Planilla(Planilla pPlanilla)
         {
             int registros = _oGestorPlanilla.CrearPLanilla(pPlanillla);
             return RedirectToAction("Planilla");
         }*/



    }
}