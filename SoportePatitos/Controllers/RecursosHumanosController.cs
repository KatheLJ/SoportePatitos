using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using SoportePatitosBD.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using SoportePatitos.Models.ViewModels;

namespace SoportePatitos.Controllers
{
    public class RecursosHumanosController : Controller
    {

        private readonly IGestorEmpleado _oGestorEmpleado;
        private readonly IGestorEvaluacion _oGestorEvaluacion;
        private readonly IGestorPlanilla _oGestorPlanilla;



        //Constructor de el empleado
        public RecursosHumanosController()
        {
            _oGestorEmpleado = new GestorEmpleado();
            _oGestorEvaluacion = new GestorEvaluacion();
            _oGestorPlanilla = new GestorPlanilla();
        }



        // GET: RecursosHumanos
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


                //Permite mostrar un dropdownlist con los puestos almacenados en la base de datos
                lst2 =
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
                ViewData["Departamento"] = items;
                ViewData["Puesto"] = items2;
                ViewData["Perfil"] = items3;
                ViewData["Horario"] = items4;
                return View();
            }
        }




        //Accion que permite Registrar un empleado en la base de datos
        public ActionResult EnviarEmpleado(Empleado pEmpleado)
        {

            int registros = _oGestorEmpleado.CrearEmpleado(pEmpleado);
            return RedirectToAction("Registro_Empleados");

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
        public ActionResult ReporteEmpleado(int Cedula)
        {
            //Evaluacion obj = _oGestorEvaluacion.ListadoEvaluacion().Where(x => x.Cedula == Cedula).FirstOrDefault();
            //Evaluacion evaluacion = _oGestorEvaluacion.EvaluacionEmpleado(Cedula);
            /*using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.Include(a => a.Evaluacion);
                empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault();
                return View(empleados);

            }*/
            return View();
           /* RepEva repEva  = _oGestorEvaluacion.ListadoEvaluacion().Where(x => x.Cedula == Cedula).FirstOrDefault(); ;

            if (ModelState.IsValid)
            {

            }
            return View(repEva);*/


        }







        //Acciones relacionadas a la planilla
        //Accion que muestra la pantalla con el reporte de la evaluacion
        public ActionResult ListadoEmpleadosPlanilla()
        {
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.Include(a => a.Puesto);
                return View(empleados.ToList());

            }
        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult CalculoPlanilla(double salarioBase)
        {
            //Calcula las deducciones de renta, basado en el salario
            double rebajoR = _oGestorPlanilla.DeducRenta(salarioBase);
            ViewBag.rebajoR = rebajoR;

            //Calcula las deducciones de seguro, basado en el salario
            double rebajoS = _oGestorPlanilla.DeducSeguro(salarioBase);
            ViewBag.rebajoS =  rebajoS;

            //Calcula las deducciones de seguro, basado en el salario
            double rebajoA = _oGestorPlanilla.DeducAusencias(salarioBase); //, cantidadDiasAusentes);
            ViewBag.rebajoA = rebajoA;

            //Calcula el salario final, luego de las deducciones
            double salarioFinal = _oGestorPlanilla.CalSalarioFinal(salarioBase, rebajoR, rebajoS, rebajoA);
            ViewBag.salariofinal = salarioFinal;



            return View();

        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult EnviarPlanilla(Planilla pPlanilla)
        {
            int registros = _oGestorPlanilla.CrearPlanilla(pPlanilla);
            return RedirectToAction("ListadoEmpleadosPlanilla"); //Cambiar a un listado de planillas
        }






        /* public ActionResult Seguro(double salarioBase)
         {
             double rebajoS = _oGestorPlanilla.DeducSeguro(salarioBase);
             ViewData["Seguro"] = rebajoS;
             return RedirectToAction("CalculoPlanilla");
         }*/


        /*public ActionResult Ausencias(double salarioBase, int cantidadDiasAusentes)
        {
            double rebajoA = _oGestorPlanilla.DeducAusencias(salarioBase, cantidadDiasAusentes);
            ViewData["Ausencias"] = rebajoA;
            return RedirectToAction("ListadoEmpleadosPlanilla");
        }*/



        /* public ActionResult CreatorPDF(string consecutive)
         {
             Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
             foreach (var key in Request.Cookies.AllKeys)
             {
                 cookieCollection.Add(key, Request.Cookies.Get(key).Value);
             }
             string nameFile = consecutive.ToLower().Contains("cot") ? string.Format("{0}.pdf", consecutive) :
                 string.Format("{0}-{1}.pdf", ConfigurationManager.AppSettings["PrefixEstablishment"], consecutive);
             var pdf = new ActionAsPdf(string.Format("OrderInvoice/{0}", consecutive))
             {
                 Cookies = cookieCollection,
                 PageSize = Rotativa.Options.Size.A4,
                 CustomSwitches = "--print-media-type",
                 PageMargins = { Left = 1, Right = 1 },
                 FileName = nameFile,
             };
             return pdf;
         }*/


    }
}