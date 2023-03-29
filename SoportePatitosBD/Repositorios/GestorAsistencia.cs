using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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



        //Método para validar cuando un empleado omite una marca o está ausente
        int IGestorAsistencia.ValidarAsistencia(int Cedula)
        {


            int n = 0;
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                ContextoBD.Evaluacion.Count();//debe contar la cèdula
                //Pasar la cantidad contada a la variable n
            }
            return n;

        }

    }
}
