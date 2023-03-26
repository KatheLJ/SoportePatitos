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

        //Permite traer los parametros para la planilla
        int ParamPlanilla();

        //Permite realizar la deducción de ausencias
        double DeducAusencias(double salarioBase, int cantidadDiasAusentes);
        //?? Se debería ligar a la parte de ausencias?

        //Permite realizar las deducciones fiscales
        double DeducRenta(double salarioBase); //int hijos, bool Casado);

        //Permite realizar las deducciones de la CCSS
        double DeducSeguro(double salarioBase);

        //Permite calcular el salario final
        double CalSalarioFinal(double DeducAusencias, double DeducRenta, double DeducSeguro);
    }
}
