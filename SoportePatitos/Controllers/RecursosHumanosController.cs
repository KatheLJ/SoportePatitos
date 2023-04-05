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
using System.Web.Optimization;

namespace SoportePatitos.Controllers
{
    public class RecursosHumanosController : Controller
    {

        //Gestores necesarios para cada una de las acciones (permiten accesar a los métodos generales del CRUD)
        private readonly IGestorEmpleado _oGestorEmpleado;
        private readonly IGestorEvaluacion _oGestorEvaluacion;
        private readonly IGestorPlanilla _oGestorPlanilla;



        //Constructores necesarios para realizar diferentes acciones
        public RecursosHumanosController()
        {
            _oGestorEmpleado = new GestorEmpleado();
            _oGestorEvaluacion = new GestorEvaluacion();
            _oGestorPlanilla = new GestorPlanilla();
        }



        // GET: RecursosHumanos

        //****************************************************************************************************************************//
        //**************************************Acciones relacionadas al registro de empleados***************************************//

        //Accion que permite mostrar el formulario para que el empleado se registre
        public ActionResult Registro_Empleados()
        {

            //Se inicializan las listas que se usaran más adelante
            List<Models.ViewModels.Departamento> lst = null;
            List<Models.ViewModels.Puesto> lst2 = null;
            List<Models.ViewModels.Perfil> lst3 = null;
            List<Models.ViewModels.Horario> lst4 = null;
            List<Models.ViewModels.Estado_civil> lst5 = null;

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

                //Permite mostrar un dropdownlist con los estados civiles almacenados en la base de datos
                lst5 =
                (from d in ContextoBD.Estado_Civil
                 select new Models.ViewModels.Estado_civil
                 {
                     ID_Estado_Civil = d.ID_Estado_Civil,
                     Descripcion = d.Descripcion
                 }).ToList();

                List<SelectListItem> items5 = lst5.ConvertAll(d =>
                {
                    return new SelectListItem
                    {

                        Text = d.Descripcion.ToString(),
                        Value = d.ID_Estado_Civil.ToString(),
                        Selected = false
                    };

                });


                ViewData["Departamento"] = items;
                ViewData["Puesto"] = items2;
                ViewData["Perfil"] = items3;
                ViewData["Horario"] = items4;
                ViewData["Estado_civil"] = items5;
                return View();
            }
        }



        //Accion que permite Registrar un empleado en la base de datos
        public ActionResult EnviarEmpleado(Empleado pEmpleado)
        {

            int registros = _oGestorEmpleado.CrearEmpleado(pEmpleado);
            return RedirectToAction("Registro_Empleados");
            
            

        }
        //****************************************************************************************************************************//














        //****************************************************************************************************************************//
        //**************************************Acciones relacionadas a la evaluación de empleados***********************************//


        //Accion que muestra la pantalla de para evaluar a un empleado
        public ActionResult Evaluacion()
        {

            return View();
        }


        
        //Accion envia la evaluación realizada a un empleado
        public ActionResult EnviarEvaluacion(Evaluacion pEvaluacion)
        {
            int registros = _oGestorEvaluacion.CrearEvaluacion(pEvaluacion);
            return RedirectToAction("Evaluacion");
        }



        //Accion que muestra la pantalla con el listado de los empleados, donde se puede ver y descargar el reporte de la evaluación
        public ActionResult ListadoEmpleados()
        {
            IEnumerable<Empleado> empleados = _oGestorEmpleado.ListadoEmpleados();
            return View(empleados);
        }



        //Accion que muestra la pantalla con el reporte de la evaluacion
        public ActionResult ReporteEmpleado(int Cedula)
        {
            
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Evaluacion.Include(a => a.Empleado);
                return View(empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
            

        }


        //Permite descargar la evaluación del empleado en PDF 
        public ActionResult GeneratePDFReporte(int Cedula)
        {
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Evaluacion.Include(a => a.Empleado);
                empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault();
                return new Rotativa.ActionAsPdf("ReporteEmpleado", empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
        }

        //****************************************************************************************************************************//

















        //****************************************************************************************************************************//
        //**********************************************Acciones relacionadas a la planilla******************************************//


        //Accion que muestra la pantalla con los diferentes empleados a los que se debe realizar la planilla
        public ActionResult ListadoEmpleadosPlanilla()
        {

            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.Include(a => a.Puesto).Include(a => a.Estado_Civil);
                return View(empleados.ToList());

            }

        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult CalculoPlanilla(double salarioBase, int Cedula, int Cantidad_Hijos, int ID_Estado_Civil)
        {

            ViewBag.salariobase = salarioBase;
            ViewBag.Cedula = Cedula;

            /* ViewBag.hijos = Cantidad_Hijos;
             ViewBag.ID_Estado_Civil = ID_Estado_Civil;

            */


            //Calcula las deducciones de renta, basado en el salario
            double rebajoR = _oGestorPlanilla.DeducRenta(salarioBase, Cantidad_Hijos, ID_Estado_Civil);
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

            //Empleado obj = _oGestorEmpleado.ListadoEmpleados().Where(x => x.Cedula == Cedula).FirstOrDefault();
            // return View(obj);

           
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.Include(a => a.Puesto).Include(a => a.Estado_Civil);
                return View(empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
            //return View();

        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult EnviarPlanilla(Planilla pPlanilla)
        {

            int registros = _oGestorPlanilla.CrearPlanilla(pPlanilla);
            return RedirectToAction("ListadoEmpleadosPlanilla"); //Cambiar a un listado de planillas

        }
        //****************************************************************************************************************************//

















        //****************************************************************************************************************************//
        //*********************************Acciones relacionadas a la descarga en PDf de planilllas***********************************//

        //Permite mostrar un listado de todas las planillas registradas en el sistema (todos los empleados)
        public ActionResult ListadoPlanilla()
        {

            IEnumerable<Planilla> planillas = _oGestorPlanilla.ListadoPlanilla();
            return View(planillas);

        }



        //Permite descargar todas las planillas registradas en el sistema
        public ActionResult PdfPlanilla()
        {

            return new Rotativa.ActionAsPdf("ListadoPlanilla");

        }

        //****************************************************************************************************************************//





        /*public ActionResult DeducRenta(double salarioBase, int Cantidad_Hijos, int ID_Estado_Civil)
        {
            double rebajoR = _oGestorPlanilla.DeducRenta(salarioBase, Cantidad_Hijos, ID_Estado_Civil);
            return rebajoR;
        }



        public ActionResult Renta(double salarioBase, int Cantidad_Hijos, int ID_Estado_Civil)
        {
            double rebajoR = _oGestorPlanilla.DeducRenta(salarioBase, Cantidad_Hijos, ID_Estado_Civil);
            ViewBag.rebajoR = rebajoR;
            return View(rebajoR);
        }*/






    }
}