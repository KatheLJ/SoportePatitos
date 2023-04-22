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
using System.Data.Entity;

namespace SoportePatitos.Controllers
{
    public class RecursosHumanosController : Controller
    {

        //Gestores necesarios para cada una de las acciones (permiten accesar a los métodos generales del CRUD)
        private readonly IGestorEmpleado _oGestorEmpleado;
        private readonly IGestorEvaluacion _oGestorEvaluacion;
        private readonly IGestorPlanilla _oGestorPlanilla;
        private readonly IGestorAsistencia _oGestorAsistencia;



        //Constructores necesarios para realizar diferentes acciones
        public RecursosHumanosController()
        {
            //Se inicializan para poder utilizar los métodos de dichas clases (métodos descritos en las interfaces)
            _oGestorEmpleado = new GestorEmpleado();
            _oGestorEvaluacion = new GestorEvaluacion();
            _oGestorPlanilla = new GestorPlanilla();
            _oGestorAsistencia = new GestorAsistencia();
        }

        
        // GET: RecursosHumanos

        //****************************************************************************************************************************//
        //**************************************Acciones relacionadas al registro de empleados***************************************//

        //Accion que permite mostrar el formulario para que el empleado se registre
        public ActionResult Registro_Empleados()
        {
            //Si la sesión es nula lleva al login
            if (Session["Cedula"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Se inicializan las listas que se usaran más adelante para que el usuario pueda ver un datos y no números.
            //Ej: En lugar de ver departamento 1 ve departamento de Gerencia
            List<Models.ViewModels.Departamento> lst = null;
            List<Models.ViewModels.Puesto> lst2 = null;
            List<Models.ViewModels.Perfil> lst3 = null;
            List<Models.ViewModels.Horario> lst4 = null;
            List<Models.ViewModels.Estado_civil> lst5 = null;

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los departamentos almacenados en la base de datos
                lst =
                //De los registros encontrados en la base de datos en la tabla de Departamento
                (from d in ContextoBD.Departamento
                 //Seleccionar un nuevo Modelo de View Models de Tipo Departamento
                 select new Models.ViewModels.Departamento
                 {
                     //Que tiene de atributos un ID y un descripción
                     ID_departamento = d.ID_departamento,
                     Descripcion = d.Descripcion
                     //Y pasarlo a una lista
                 }).ToList();

                //Seleccionar item de la lista
                List<SelectListItem> items = lst.ConvertAll(d =>
                {
                    //Regresar ese item
                    return new SelectListItem
                    {
                        //Que tiene por texto la descripción, pero su valor real es el ID
                        Text = d.Descripcion.ToString(),
                        Value = d.ID_departamento.ToString(),
                        Selected = false
                    };

                });


                //Permite mostrar un dropdownlist con los puestos almacenados en la base de datos
                //Misma Lógica que la lista de Departamento (lst)
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
                //Misma Lógica que la lista de Departamento (lst)
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
                //Misma Lógica que la lista de Departamento (lst)
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
                //Misma Lógica que la lista de Departamento (lst)
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

                //Se regresan las listas por medio de ViewData, para que se puedan llamar en las vistas
                ViewData["Departamento"] = items;
                ViewData["Puesto"] = items2;
                ViewData["Perfil"] = items3;
                ViewData["Horario"] = items4;
                ViewData["Estado_civil"] = items5;
                //Se retorna la vista
                return View();
            }
        }



        //Accion que permite Registrar un empleado en la base de datos
        public ActionResult EnviarEmpleado(Empleado pEmpleado)
        {
            try
            {
                //Se llama al método de Crear Empleado, que recibe un objeto de tipo empleado por parámetro
                int registros = _oGestorEmpleado.CrearEmpleado(pEmpleado);
                if (registros > 0)
                {
                    //Se regresa a la pantalla de Registro de Empleados al terminar y se muestra el mensaje exitoso
                    TempData["RegistroCorrecto"] = "Empleado Registrado Correctamente";
                    //Se remueve la alerta del error
                    TempData.Remove("RegistroError");
                }
                else
                {
                    //Se muestra el error
                    TempData["RegistroError"] = "La cédula ingresada ya existe. Intente nuevamente.";
                    TempData.Remove("RegistroCorrecto");
                }
                return RedirectToAction("Registro_Empleados");
            }

            catch (Exception ex)
            {
                //Se muestra el error
                TempData["RegistroError"] = "No se pudo crear el empleado. Intente nuevamente.";
                TempData.Remove("RegistroCorrecto");
                return RedirectToAction("Registro_Empleados");
            }
        }

        //****************************************************************************************************************************//














        //****************************************************************************************************************************//
        //**************************************Acciones relacionadas a la evaluación de empleados***********************************//


        //Accion que muestra la pantalla de para evaluar a un empleado
        public ActionResult Evaluacion()
        {
            //Si la sesión es nula lleva al login
            if (Session["Cedula"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Models.ViewModels.EmpleadoData> lst1 = null;

            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los empleados almacenados en la base de datos
                //Se utiliza para que en la vista el usuario no pueda digitar la cédula, sino que tenga que seleccionar una de las que están en la lista 
                lst1 =
                //De los registros encontrados en la base de datos en la tabla de Empleados
                (from d in ContextoBD.Empleado
                //Seleccionar un nuevo Modelo de View Models de Tipo Empleado
                 select new Models.ViewModels.EmpleadoData
                 {
                     //Que tiene de atributos Cédula y un nombre
                     Cedula = d.Cedula,
                     Nombre_Empleado = d.Nombre_Empleado
                     //Y pasarlo a una lista
                 }).ToList();

                //Seleccionar un item de la lista
                List<SelectListItem> items = lst1.ConvertAll(d =>
                {
                    //Regresar este item
                    return new SelectListItem
                    {
                        //Que tiene por texto el nombre, pero su valor real es el Cédula
                        Text = d.Nombre_Empleado.ToString(),
                        Value = d.Cedula.ToString(),
                        Selected = false
                    };

                });
                //Se regresa la lista de items por ViewData, para que se pueda pasar luego a las vistas
                ViewData["Empleados"] = items;
                //Se regresa la vista
                return View();
            }
                
        }


        
        //Accion envia la evaluación realizada a un empleado
        public ActionResult EnviarEvaluacion(Evaluacion pEvaluacion)
        {
            try
            {
                //Se llama al método de Crear Evaluación, que recibe un objeto de tipo evaluación por parámetro
                int registros = _oGestorEvaluacion.CrearEvaluacion(pEvaluacion);
                if (registros > 0)
                {
                    //Se regresa a la pantalla de Registro de Empleados al terminar y se muestra el mensaje de exitoso
                    TempData["EvaCorrecto"] = "Evaluación Creada Correctamente";
                    //Se remueve la alerta del error
                    TempData.Remove("EvaError");
                }
                else
                {
                    //Se muestra el error
                    TempData["EvaError"] = "Error al crear la evaluación. Intente nuevamente.";
                    //Se remueve la alerta exitosa
                    TempData.Remove("EvaCorrecto");
                }
                //Se regresa a la pantalla de Evaluación al terminar
                return RedirectToAction("Evaluacion");
            }

            catch (Exception ex)
            {
                //Se muestra el error
                TempData["EvaError"] = "Error al crear la evaluación. Intente nuevamente.";
                //Se remueve la alerta exitosa
                TempData.Remove("EvaCorrecto");
                //Se regresa a la pantalla de Evaluación al terminar
                return RedirectToAction("Evaluacion");
            }

        }



        //Accion que muestra la pantalla con el listado de los empleados, donde se puede ver y descargar el reporte de la evaluación
        public ActionResult ListadoEmpleados()
        {
            //Si la sesión es nula lleva al login
            if (Session["Cedula"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.Include(a => a.Departamento).Include(a => a.Puesto).Include(a => a.Horario).Include(a => a.Perfil);
                return View(empleados.ToList());
            }
        }



        //Accion que muestra la pantalla con el reporte de la evaluacion
        public ActionResult ReporteEmpleado(int Cedula)
        {
            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se hace una variable empleados para llamar a la conexión ContextoBD, tabla Evaluación
                //Y con el Include se puede vincular otra tabla que sería la de empleado
                var empleados = ContextoBD.Evaluacion.Include(a => a.Empleado);
                //Se regresa la vista con la lista de empleados, donde la cédula sea igual a la cédula pasada por parámetro (solo el primer registro)
                return View(empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
            

        }


        //Permite descargar la evaluación del empleado en PDF 
        public ActionResult GeneratePDFReporte(int Cedula)
        {
            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se llama a la tabla de Evaluación, se pasa a lista, pero que solo se muestre el primer registro según la cédula pasada por parámetro
                var empleados = ContextoBD.Evaluacion.Include(a => a.Empleado);
                empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault();
                //Se regresa la vista pero como PDF
                return new Rotativa.ActionAsPdf("ReporteEmpleado", empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
        }

        //****************************************************************************************************************************//

















        //****************************************************************************************************************************//
        //**********************************************Acciones relacionadas a la planilla******************************************//


        //Accion que muestra la pantalla con los diferentes empleados a los que se debe realizar la planilla
        public ActionResult ListadoEmpleadosPlanilla()
        {
            //Si la sesión es nula lleva al login
            if (Session["Cedula"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se hace una variable empleados para llamar a la conexión ContextoBD, tabla Empleado
                //Y con el Include se puede vincular otra tabla que sería la de puesto y la de estado civil
                var empleados = ContextoBD.Empleado.Include(a => a.Puesto).Include(a => a.Estado_Civil);
                return View(empleados.ToList());

            }

        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult CalculoPlanilla(double salarioBase, int Cedula, int Cantidad_Hijos, int ID_Estado_Civil)
        {
            //Se traen los datos de salario y cédula
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

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se hace una variable empleados para llamar a la conexión ContextoBD, tabla Empleado
                //Y con el Include se puede vincular otra tabla que sería la de puesto y la de estado civil
                var empleados = ContextoBD.Empleado.Include(a => a.Puesto).Include(a => a.Estado_Civil);
                //Se regresa la vista con la lista
                return View(empleados.ToList().Where(x => x.Cedula == Cedula).FirstOrDefault());
            }
            //return View();

        }


        //Accion que muestra la pantalla en donde se maneja la planilla
        public ActionResult EnviarPlanilla(Planilla pPlanilla)
        {

            //Se crea una variable registros, para llamar al método de Crear Planilla (de Gestor planilla)
            //Se pasa por parámetro un objeto de tipo planilla
            int registros = _oGestorPlanilla.CrearPlanilla(pPlanilla);
            //Se redirecciona a la pantalla de ListadoEmpleadosPlanilla
            return RedirectToAction("ListadoPlanilla");

        }
        //****************************************************************************************************************************//

















        //****************************************************************************************************************************//
        //*********************************Acciones relacionadas a la descarga en PDf de planilllas***********************************//

        //Permite mostrar un listado de todas las planillas registradas en el sistema (todos los empleados)
        public ActionResult ListadoPlanilla()
        {
            
            //Muestra una lista de todas las planillas del sistema, llamando al método de listado planilla del gestor
            IEnumerable<Planilla> planillas = _oGestorPlanilla.ListadoPlanilla();
            //Se regresa la vista con dicha lista
            return View(planillas);

        }



        //Permite descargar todas las planillas registradas en el sistema
        public ActionResult PdfPlanilla()
        {

            //Regresa un PDF con los datos de la vista respectiva que es ListadoPlanilla
            return new Rotativa.ActionAsPdf("ListadoPlanilla");

        }

        //****************************************************************************************************************************//




        //Acciones por eliminar 
        public ActionResult DeducRenta()
        {
            return View();
        }
        public ActionResult Renta(double salarioBase, int Cantidad_Hijos, int ID_Estado_Civil)
        {
            double rebajoR = _oGestorPlanilla.DeducRenta(salarioBase, Cantidad_Hijos, ID_Estado_Civil);
            ViewBag.rebajoR = rebajoR;
            return RedirectToAction("DeducRenta");
        }





        //****************************************************************************************************************************//
        //*********************************Acciones relacionadas a la asistencia de los empleados************************************//

        //Permite ver todas las marcas realizadas durante el día
        public ActionResult ListadoAsistencia()
        {
            //Si la sesión es nula lleva al login
            if (Session["Cedula"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se crea un variable asistencia que trae todos los registros de la tabla de Asistencia de la BD con su estado respectivo
                var asistencia  = ContextoBD.Asistencia.Include(a => a.Estado);
                return View(asistencia.ToList());

            }
        }



        //Permite validar las ausencias que tuvieron los empleados a lo largo del día
        public ActionResult ActualizarAsistencia()
        {
            //Se crea una variable para llamar al Gestor
            var gestorAsistencia = new GestorAsistencia();
            //Se llama al método requerido para que se actualicen los estados de los usuarios
            gestorAsistencia.ActualizarAsistencia();
            //Se regresa a la vista para mostrar los cambios realizados
            return RedirectToAction("ListadoAsistencia");
        }



    }
}