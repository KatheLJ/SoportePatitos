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
    public class GestorEvaluacion : IGestorEvaluacion
    {
        //Método para enlistar los Evaluacions creados (Mantenimiento)
        IEnumerable<Evaluacion> IGestorEvaluacion.ListadoEvaluacion()
        {
            //Se inicializa una nueva lista
            List<Evaluacion> Evaluacions = new List<Evaluacion>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Evaluación de la BD a una Lista 
                Evaluacions = ContextoBD.Evaluacion.ToList();
            }

            //Se regresa la lista
            return Evaluacions;
        }



        //Método para crear nuevas evaluaciones
        int IGestorEvaluacion.CrearEvaluacion(Evaluacion pEvaluacion)

        {
            //Intenta el código
            try
            {
                int n = 0;
                //Utiliza está conexión a la base de datos
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    //Añade un objeto de tipo Evaluación
                    ContextoBD.Evaluacion.Add(pEvaluacion);
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

        int IGestorEvaluacion.EvaluacionEmpleado(int Cedula) {


             int n = 0;
             using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
             {
                ContextoBD.spEvaluacionEmpleado().Where(x => x.Cedula == Cedula).FirstOrDefault(); ;
                n = ContextoBD.SaveChanges();
             }
             return n;

         }


        

    }
}
