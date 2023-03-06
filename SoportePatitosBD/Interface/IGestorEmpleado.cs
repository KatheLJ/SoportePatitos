using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoportePatitosBD.Modelo;

namespace SoportePatitosBD.Interface
{
    public interface IGestorEmpleado
    {
        //Permite crear un listado de todos los empleados registrados en el sistema
        IEnumerable<Empleado> ListadoEmpleados();

        //Método que permite crear un empleado
        int CrearEmpleado(Empleado pEmpleado);

        //Método que permite actualizar un empleado
        int ActualizarEmpleado(Empleado pEmpleado);

        //Método que permite Eliminar un empleado
        int EliminarEmpleado(int pIdEmpleado);


        //Listas para los dropdown list necesarios en la pantalla de registro
        IEnumerable<Departamento> ListadoDepartamento();
        IEnumerable<Puesto> ListadoPuesto();
        IEnumerable<Perfil> ListadoPerfil();
        IEnumerable<Horario> ListadoHorario();

    }
}
