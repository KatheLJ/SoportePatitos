using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Forms;


namespace SoportePatitosBD.Repositorios
{
    public class GestorAsistencia : IGestorAsistencia
    {

        //Método para enlistar los Asistencias creados (Mantenimiento)
        IEnumerable<Asistencia> IGestorAsistencia.ListadoAsistencia()
        {
            //Se inicializa una nueva lista
            List<Asistencia> Asistencias = new List<Asistencia>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Asistencia de la BD a una Lista 
                Asistencias = ContextoBD.Asistencia.ToList();
            }
            //Se regresa la lista
            return Asistencias;
        }

        bool IGestorAsistencia.VerificarMarca(int cedula, DateTime fecha)
        {
            using (SoportePatitosEntities contextoBD = new SoportePatitosEntities())
            {
                // Obtener la fecha sin la hora
                DateTime fechaSinHora = fecha.Date;

                // Verificar si hay una asistencia registrada para la cédula y fecha especificadas
                bool hayMarca = contextoBD.Asistencia.Where(a => a.Cedula == cedula).ToList().Any(a => a.Fecha.Date == fecha.Date);
                return hayMarca;
            }
        }

        //Método para contar marcas
        public int ContarMarcas(int cedula, DateTime fecha)
        {
            // Implementación del método
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                DateTime fechaTruncada = fecha.Date;
                return ContextoBD.Asistencia.Count(a => a.Cedula == cedula && DbFunctions.TruncateTime(a.Fecha) == fechaTruncada);
            }
        }



        //Método para crear nuevos Asistencias 
        int IGestorAsistencia.CrearAsistencia(Asistencia pAsistencia)
        {
            try
            {
                int n = 0;
                //Utiliza está conexión a la base de datos
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    // Verificar si ya hay dos marcas para la fecha y cédula especificadas
                    DateTime fecha = pAsistencia.Fecha.Date;
                    //Contarlas solo con base en la fecha, sin la hora
                    int marcasCount = ContextoBD.Asistencia.Count(a => a.Cedula == pAsistencia.Cedula && DbFunctions.TruncateTime(a.Fecha) == fecha);
                    //Si las marcas son mayores o iguales que 2
                    if (marcasCount >= 2)
                    {
                        return -1; // Devolver un valor indicando que no se puede registrar una nueva marca
                    }

                    //Sino agregar la nueva marca
                    ContextoBD.Asistencia.Add(pAsistencia);
                    n = ContextoBD.SaveChanges();
                }

                return n;
            }
            catch (Exception e)
            {
                //Mostrar un mensaje de error si se presenta
                Console.WriteLine(e.Message);
                throw new Exception("No se puede registrar la marca porque ya existen dos marcas para la fecha y cedula especificadas.");
            }
        }

        public void ActualizarAsistencia()
        {
            //Utiliza está conexión a la base de datos
            using (var ContextoBD = new SoportePatitosEntities())
            {
                //Se crea una variable empleados donde se crea una lista de los empleados de que están en el sistema
                var empleados = ContextoBD.Empleado.ToList();
                //Se crea una variable llamada fechaInicio, donde se busca la fecha
                var fechaInicio = ContextoBD.Asistencia.Min(a => a.Fecha).Date;
                //Se crea una variable FechaFin, donde se añade el día de hoy
                var fechaFin = DateTime.Today.AddDays(1);

                for (var fecha = fechaInicio; fecha < fechaFin; fecha = fecha.AddDays(1))
                {
                    var fechaSiguiente = fecha.AddDays(1);
                    //Para cada variable empleado en los empleados
                    foreach (var empleado in empleados)
                    {
                        var asistencias = ContextoBD.Asistencia.Where(a => a.Cedula == empleado.Cedula && a.Fecha >= fecha && a.Fecha < fechaSiguiente).ToList();
                        //Si las marcas de asistencia son igual a 2
                        if (asistencias.Count == 2)
                        {
                            //Para cada variable asistencia en la lista de asistencias
                            foreach (var asistencia in asistencias)
                            {
                                //Se asigna el estado de 1 (Presente)
                                asistencia.ID_Estado = 1;
                            }
                        }
                        //Si las marcas de asistencia son igual a 1
                        else if (asistencias.Count == 1)
                        {
                            //El estado es 9 (omisión de marca)
                            asistencias[0].ID_Estado = 9;
                        }
                        //Si las marcas de asistencia son igual a 0
                        else if (asistencias.Count == 0)
                        {
                            //Se crea una nueva asistencia si no se encuentran marcas, para indicar que el empleado estuvo ausente
                            var nuevaAsistencia = new Asistencia
                            {
                                Cedula = empleado.Cedula,
                                Fecha = fecha,
                                ID_Estado = 2,
                                Tipo = 1 
                            };
                            //Se añade la asistencia a la tabla de Asistencia
                            ContextoBD.Asistencia.Add(nuevaAsistencia);
                        }
                    }
                }
                //Se guardan los cambios
                ContextoBD.SaveChanges();
            }
        }

        //Método para validar cuando un empleado omite una marca o está ausente
        /*int IGestorAsistencia.ValidarAusencias(Asistencia pAsistencia)
        {
            DateTime fecha = DateTime.UtcNow;
            int n = 0;
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                foreach (var asistencia in ContextoBD.Asistencia)
                {
                    int marcasCount = ContextoBD.Asistencia.Count(a => a.Cedula == pAsistencia.Cedula && DbFunctions.TruncateTime(a.Fecha) == fecha);
                    if (marcasCount == 2)
                    {
                        pAsistencia.ID_Estado = 1;
                    }
                    else if (marcasCount == 1)
                    {
                        pAsistencia.ID_Estado = 9;
                    }
                    else
                    {
                        pAsistencia.ID_Estado = 2;
                    }
                   
                }
               // ContextoBD.Entry<Asistencia>(pAsistencia).State = System.Data.Entity.EntityState.Modified;
                n = ContextoBD.SaveChanges();
                return n;
            }
            

        }*/

    }
}
