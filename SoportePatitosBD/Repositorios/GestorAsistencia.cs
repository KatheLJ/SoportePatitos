using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
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



        //Método para crear nuevos Asistencias 
        int IGestorAsistencia.CrearAsistencia(Asistencia pAsistencia)

        {
            //Intenta el código
            try
            {
                int n = 0;
                //Utiliza está conexión a la base de datos
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    //Añade un objeto de tipo Evaluación
                    ContextoBD.Asistencia.Add(pAsistencia);
                    //Guarda los cambios
                    n = ContextoBD.SaveChanges();
                }
                //Regresa los datos en la variable
                return n;
            }

            //Muestra una excepción, si no funciona
            catch (DbEntityValidationException e)
            {

                Console.WriteLine(e.InnerException.Message);

                throw;
            }
        }
    }
}
