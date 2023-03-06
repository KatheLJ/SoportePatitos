using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Interface
{
    public interface IGestorAsistencia
    {

        //Permite crear la evaluación a un empleado
        int CrearAsistencia(Asistencia pAsistencia);
        //Permite hace un listado de todas las marcas de asistencia registradas en el sistema
        IEnumerable<Asistencia> ListadoAsistencia();
    }
}
