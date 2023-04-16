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

        public int ContarMarcas(int cedula, DateTime fecha)
        {
            // implementación del método
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
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    // Verificar si ya hay dos marcas para la fecha y cédula especificadas
                    DateTime fecha = pAsistencia.Fecha.Date;
                    int marcasCount = ContextoBD.Asistencia.Count(a => a.Cedula == pAsistencia.Cedula && DbFunctions.TruncateTime(a.Fecha) == fecha);
                    if (marcasCount >= 2)
                    {
                        return -1; // Devolver un valor indicando que no se puede registrar una nueva marca
                    }

                    //Agregar la nueva marca
                    ContextoBD.Asistencia.Add(pAsistencia);
                    n = ContextoBD.SaveChanges();
                }

                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("No se puede registrar la marca porque ya existen dos marcas para la fecha y cédula especificadas.");
            }
        }

        public void ActualizarAsistencia()
        {
            using (var ContextoBD = new SoportePatitosEntities())
            {
                var empleados = ContextoBD.Empleado.ToList();
                var fechaInicio = ContextoBD.Asistencia.Min(a => a.Fecha).Date;
                var fechaFin = DateTime.Today.AddDays(1);
                for (var fecha = fechaInicio; fecha < fechaFin; fecha = fecha.AddDays(1))
                {
                    var fechaSiguiente = fecha.AddDays(1);
                    foreach (var empleado in empleados)
                    {
                        var asistencias = ContextoBD.Asistencia.Where(a => a.Cedula == empleado.Cedula && a.Fecha >= fecha && a.Fecha < fechaSiguiente).ToList();
                        if (asistencias.Count == 2)
                        {
                            foreach (var asistencia in asistencias)
                            {
                                asistencia.ID_Estado = 1;
                            }
                        }
                        else if (asistencias.Count == 1)
                        {
                            asistencias[0].ID_Estado = 9;
                        }
                        else if (asistencias.Count == 0)
                        {
                            var nuevaAsistencia = new Asistencia
                            {
                                Cedula = empleado.Cedula,
                                Fecha = fecha,
                                ID_Estado = 2,
                                Tipo = 1 
                            };
                            ContextoBD.Asistencia.Add(nuevaAsistencia);
                        }
                    }
                }
                ContextoBD.SaveChanges();
            }
        }

        //Método para validar cuando un empleado omite una marca o está ausente
        int IGestorAsistencia.ValidarAusencias(Asistencia pAsistencia)
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
            

        }

    }
}
