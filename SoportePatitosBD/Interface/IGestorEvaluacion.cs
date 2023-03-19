using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Interface
{
    public interface IGestorEvaluacion
    {

        //Permite crear la evaluación a un empleado
        int CrearEvaluacion(Evaluacion pEvaluacion);

        //Permite realizar una lista de las evaluaciones de todos los empleados
        IEnumerable<Evaluacion> ListadoEvaluacion();

        //Método para poder mostrar el reporte del empleado luego de la evaluación
        int EvaluacionEmpleado(int id);

        

    }
}
