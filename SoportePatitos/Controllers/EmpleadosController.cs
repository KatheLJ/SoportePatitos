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
using System.Data.Entity;


namespace SoportePatitos.Controllers
{
    public class EmpleadosController : Controller
    {
        //Se inicializan variables para cada tipo de interfaz que se va a utilizar
        private readonly IGestorEmpleado _oGestorEmpleado;
        private readonly IGestorEvaluacion _oGestorEvaluacion;
        private readonly IGestorAsistencia _oGestorAsistencia;
        private readonly IGestorPlanilla _oGestorPlanilla;


        //Constructor de el empleado
        public EmpleadosController()
        {
            //Se inicializan para poder utilizar sus métodos
            _oGestorEmpleado = new GestorEmpleado();
            _oGestorEvaluacion = new GestorEvaluacion();
            _oGestorAsistencia = new GestorAsistencia();
            //  _oGestorPlanilla = new GestorPlanilla();
        }




        // GET: Empleado
        //****************************************************************************************************************************//
        //************************************Acciones relacionadas a la marca de asistencias ***************************************//
        
        //Accion que muestra la pantalla para registro el ingreso y la salida del empleado
        public ActionResult MarcaAsistencia()
        {
            //Se inicializan las listas que se usaran más adelante
            List<Models.ViewModels.Estado> estadolst = null;
            int n = 0;

            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Permite mostrar un dropdownlist con los los estados almacenados en la base de datos (Ya no se utiliza, pero se mantiene para futuras acciones)
                estadolst =
                (from d in ContextoBD.Estado
                 //Se llama al ViewModel Estado
                 select new Models.ViewModels.Estado
                 {
                     //Se incluyen las variables de este ViewModel
                     ID_Estado = d.ID_Estado,
                     Descripcion = d.Descripcion
                 //Y se pasan a una lista
                 }).ToList();


                //Se llama  a la lista
                List<SelectListItem> estados = estadolst.ConvertAll(d =>
                {

                    return new SelectListItem
                    {
                        //Se tiene un parámetro de tipo text que será lo que verá el usuario y uno de tipo value que será el valor real que tendrá en base de datos
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

                //Se regresa la vista 
                return View();
            }

        }



        //Accion que muestra la pantalla para enviar el ingreso/salida (Marcas)
        public ActionResult EnviarAsistencia(Asistencia pAsistencia)
        {
            //Esta variable hayMarca de tipo booleano permite verificar una marca por medio de dos parámetros, la cédula del empleado y la fecha
            bool hayMarca = _oGestorAsistencia.VerificarMarca(pAsistencia.Cedula, pAsistencia.Fecha);
            //Por medio de tipo se asigna si la marca será 1 de entrada o dos de salida
            int tipo = hayMarca ? 2 : 1;
            //Se iguala la variable al atributo Tipo de la clase Asistencia por medio de dos parámetros, la cédula del empleado y la fecha
            pAsistencia.Tipo = tipo;

            //Variable que permite contar las marcas de los empleados 
            int marcasRegistradas = _oGestorAsistencia.ContarMarcas(pAsistencia.Cedula, pAsistencia.Fecha);

            //Si las marcas son más de 2
            if (marcasRegistradas >= 2)
            {
                //Le muestra al usuario que no puede registrar más marcas ese día
                TempData["MensajeError"] = "No se puede agregar una tercera marca para esta fecha y cédula.";
                return RedirectToAction("MarcaAsistencia");
            }
            else
            {
                //Sino guarda la marca llamando al método de Crear Asistencia que recibe por parámetro a un objeto de tipo Asistencia
                int registros = _oGestorAsistencia.CrearAsistencia(pAsistencia);
                //Y redirige a la pantalla de MarcaAsistencia
                return RedirectToAction("MarcaAsistencia");
            }
        }



         
        

        //****************************************************************************************************************************//















        //****************************************************************************************************************************//
        //*****************************Acciones relacionadas a las colillas de pago y las descargas en PDF****************************//


        //Permite mostrar el historial de colillas de pago del empleado que está en sesión
        public ActionResult HistorialColilla()
        {
            //Se guarda la cédula del usuario que se encuentra el sesión
            int Cedula = (int)Session["Cedula"];
            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se crea un varias empleados que guardará los datos de la planilla y el empleado (el Include permite vincular dichas tablas)
                var empleados = ContextoBD.Planilla.Include(a => a.Empleado);
                //Se retorna la vista, pero con la variable empleados como parámetro, donde se pasa a lista y deben ser solo aquellos registros que sean iguales
                //entre la Cédula del objeto y la cédula guardada en la sesión 
                return View(empleados.ToList().Where(x => x.Cedula == Cedula));
            }

        }



        //Permite mostrar una colilla de pago específica (usuario en sesión)
        public ActionResult Colillas(int ID_planilla)
        {
            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se crea un varias colilla que guardará los datos de la planilla y el empleado (el Include permite vincular dichas tablas)
                var colilla = ContextoBD.Planilla.Include(a => a.Empleado);
                //Se retorna la vista, pero con la variable colilla como parámetro, donde se pasa a lista y deben ser solo aquellos registros que sean iguales
                //entre el id de planilla pasado como parámetro y el id de planilla del objeto, pero mostrando solo el primero (FirstOrDefault)
                return View(colilla.ToList().Where(x => x.ID_planilla == ID_planilla).FirstOrDefault());
            }


        }


        //Permite descargar una colilla de pago específica (usuario en sesión)
        public ActionResult GenerarPDF(int ID_planilla)
        {
            //Se llama a una conexión de tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se realiza un proceso muy similar al de las Colillas
                var colilla = ContextoBD.Planilla.Include(a => a.Empleado);
                colilla.ToList().Where(x => x.ID_planilla == ID_planilla).FirstOrDefault();
                //Pero se retorna una Acción de tipo PDF usando Rotativa, que recibe la vista de Colillas, junto con la la colilla.ToList()
                return new Rotativa.ActionAsPdf("Colillas", colilla.ToList().Where(x => x.ID_planilla == ID_planilla).FirstOrDefault());
            }

        }

        //****************************************************************************************************************************//


    }
}
