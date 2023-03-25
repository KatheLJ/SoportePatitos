using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Interface
{
    public interface IGestorPlanilla
    {
        //Permite crear la planilla
        // int CrearPlanilla(Planilla pPlanilla);

        //Permite traer los parametros para la planill
        int ParamPlanilla();

        //Permite realizar la deducción de ausencias
        int DeducAusencias(int salarioBase, int cantidadDiasAusentes);


        //Permite realizar las deducciones fiscales
        int DeducRenta();

        //Permite realizar las deducciones de la CCSS
        int DeducSeguro();

        //Permite calcular el salario final
        int CalSalarioFinal(int DeducAusencias, int DeducRenta, int DeducSeguro);
    }
}
