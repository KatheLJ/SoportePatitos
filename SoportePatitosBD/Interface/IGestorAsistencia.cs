﻿using SoportePatitosBD.Modelo;
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

        //Método para validar cuando un empleado omite una marca o está ausente
        //int ValidarAusencias(Asistencia pAsistencia);

        //Permite verificar las marcas
        bool VerificarMarca(int cedula, DateTime fecha);

        //Permite Contar las marcas, según la cédula y la fecha
        int ContarMarcas(int cedula, DateTime fecha);

        //Permite actualizra la asistencia (poner ausencias/omisiones/estado presente)
        void ActualizarAsistencia();

    }
}
