using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoportePatitos.Models.ViewModels
{
    public class EmpleadoData
    {
        public int Cedula { get; set; }
        public string Nombre_Empleado { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public int ID_departamento { get; set; }
        public int ID_perfil { get; set; }
        public int ID_puesto { get; set; }
        public int ID_horario { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int Cantidad_Hijos { get; set; }
        public int ID_Estado_Civil { get; set; }
    }
}