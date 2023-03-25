using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Repositorios
{
    public class GestorPlanilla : IGestorPlanilla
    {

        //Permite realizar la deducción de ausencias
        int IGestorPlanilla.DeducAusencias(int salarioBase, int cantidadDiasAusentes)
        {
            int deducciones = salarioBase * cantidadDiasAusentes / 30;
            return deducciones;
        }



        //Permite realizar las deducciones fiscales
        int IGestorPlanilla.DeducRenta()
        {
            return 0;
        }


        //Permite realizar las deducciones de la CCSS
        int IGestorPlanilla.DeducSeguro()
        {
            return 0;
        }


        //Permite calcular el salario final
        int IGestorPlanilla.CalSalarioFinal(int DeducAusencias, int DeducRenta,int DeducSeguro)
        {
            int salario = 0;
            salario = salario - DeducAusencias - DeducRenta - DeducSeguro;
            return salario;
        }


        //Trae parametros de otras tablas, requeridos para la planilla
        int IGestorPlanilla.ParamPlanilla()
        {

            int n = 0;
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //ContextoBD.spPLanillla();
                n = ContextoBD.SaveChanges();
            }
            return n;

        }


    }
}
