using SoportePatitosBD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoportePatitosBD.Modelo;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SoportePatitosBD.Repositorios
{
    public class GestorEmpleado : IGestorEmpleado
    {
        //Método para enlistar los Empleados creados (Mantenimiento)
        IEnumerable<Empleado> IGestorEmpleado.ListadoEmpleados()
        {
            List<Empleado> Empleados = new List<Empleado>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Empleado de la BD a una Lista 
                Empleados = ContextoBD.Empleado.ToList();
            }
            //Se regresa la lista
            return Empleados;
        }


        
        //Método para crear nuevos Empleados 
        int IGestorEmpleado.CrearEmpleado(Empleado pEmpleado)
        {
            //Intenta el código
            
                int n = 0;
                //Utiliza está conexión a la base de datos
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    try
                    {
                        ContextoBD.Empleado.Add(pEmpleado);
                        n = ContextoBD.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ocurrió un error al agregar el registro de empleado: " + ex.Message);
                    }
                }
                return n;
            
        }


    



    //Método para actualizar un Empleado (Mantenimiento)
    int IGestorEmpleado.ActualizarEmpleado(Empleado pEmpleado)
        {
            int n = 0;
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                ContextoBD.Entry<Empleado>(pEmpleado).State = System.Data.Entity.EntityState.Modified;
                //Guarda los cambios
                n = ContextoBD.SaveChanges();
            }
            return n;

        }

        //Método para eliminar un Empleado (Mantenimiento)
        int IGestorEmpleado.EliminarEmpleado(int pIdEmpleado)
        {
            int n = 0;
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                var obj = ContextoBD.Empleado.Where(x => x.Cedula == pIdEmpleado).FirstOrDefault();
                if (obj == null)
                {
                    n = 0;
                }
                else
                {
                    ContextoBD.Entry<Empleado>(obj).State = System.Data.Entity.EntityState.Deleted;
                    //Guarda los cambios
                    n = ContextoBD.SaveChanges();
                }
            }
            return n;
        }


        //Método para mostrar todos los departamentoz
        IEnumerable<Departamento> IGestorEmpleado.ListadoDepartamento() 
        {
            List<Departamento> Departamentos = new List<Departamento>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Departamentos de la BD a una Lista 
                Departamentos = ContextoBD.Departamento.ToList();
            }
            //Se regresa la lista
            return Departamentos;
        }



        //Método para mostrar todos los puestos
        IEnumerable<Puesto> IGestorEmpleado.ListadoPuesto()
        {
            List<Puesto> Puestos = new List<Puesto>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Puestos de la BD a una Lista 
                Puestos = ContextoBD.Puesto.ToList();
            }
            //Se regresa la lista
            return Puestos;
        }

        //Método para mostrar todos los perfiles
        IEnumerable<Perfil> IGestorEmpleado.ListadoPerfil()
        {
            List<Perfil> Perfiles = new List<Perfil>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Perfiles de la BD a una Lista 
                Perfiles = ContextoBD.Perfil.ToList();
            }
            //Se regresa la lista
            return Perfiles;
        }

        //Método para mostrar todos los horarios
        IEnumerable<Horario> IGestorEmpleado.ListadoHorario()
        {
            List<Horario> Horarios = new List<Horario>();
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //Se pasa la tabla de Horarios de la BD a una Lista 
                Horarios = ContextoBD.Horario.ToList();
            }
            //Se regresa la lista
            return Horarios;
        }


    }
}
