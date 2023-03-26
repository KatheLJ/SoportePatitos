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
        double IGestorPlanilla.DeducAusencias(double salarioBase, int cantidadDiasAusentes)
        {
            double deducciones = (salarioBase/30) * cantidadDiasAusentes;
            return deducciones;
        }



        //Permite realizar las deducciones fiscales
        double IGestorPlanilla.DeducRenta(double salarioBase)//  int hijos, bool Casado)
        {
           
            //Variables a utilizar
            double exento = 941000.00;
            double Salario_impuesto = 0.00;
            double Salario_impuesto1 = 0.00;
            double Salario_impuesto2 = 0.00;
            double Salario_impuesto3 = 0.00;
            double monto1 = 440000;
            double monto2 = 1042000;
            double monto3 = 2422000;
            double monto4 = 95154999;
            double porcentaje1 = 0.10;
            double porcentaje2 = 0.15;
            double porcentaje3 = 0.20;
            double porcentaje4 = 0.25;
            double monto = 0.0;
            double Impuesto = 0.0;
            double Impuesto1 = 0.0;
            double Impuesto2 = 0.0;
            double Impuesto3 = 0.0;
            double Impuesto4 = 0.0;



    
            
            //Si el salario base es menor que el monto exento de 941000, el impuesto de renta es 0
            if (salarioBase < exento)
            {
                Impuesto = 0.00;
                return Impuesto;
                

            }
            //Pero si el salario es mayor al exento
            else if (salarioBase > exento)
            {
                //Se calcula el salario luego de quitar el exento de 941000.00
                Salario_impuesto = salarioBase - exento;
               // return Salario_impuesto;


                //Si el Salario es menor que el monto 1 
                if (Salario_impuesto < monto1)
                {
                    //El impuesto se calcula sobre el salario restante en caso de 
                    Impuesto1 = Salario_impuesto * porcentaje1;
                    Impuesto = Impuesto + Impuesto1;
                    return Impuesto;
                }
                //Pero si el salario es mayor al monto
                else if (Salario_impuesto > monto1)
                {
                    //El impuesto se calcula sobre el monto 
                    Impuesto1 = monto1 * porcentaje1;
                    //return Impuesto1;
                    Salario_impuesto1 = Salario_impuesto - monto1;
                    //return Salario_impuesto;
                    //El impuesto va a ser el impuesto que ya se tenga + el valor del impuesto 1 
                    Impuesto = Impuesto + Impuesto1;

                    //Si el Salario restante es menor que el monto 2
                    if (Salario_impuesto1 < monto2)
                    {
                        //El impuesto se calcula sobre el restante
                        Impuesto2 = Salario_impuesto1 * porcentaje2;
                        Impuesto = Impuesto + Impuesto2;
                        return Impuesto;


                    }
                    //Perso si es mayor que el monto 2
                    else if (Salario_impuesto1 > monto2)
                    {
                        //El impuesto se calcula sobre el monto 2
                        Impuesto2 = monto2 * porcentaje2;
                        Salario_impuesto2 = Salario_impuesto1 - monto2;
                        //El impuesto va a ser el impuesto que ya se tenga + el valor del impuesto 2
                        Impuesto = Impuesto + Impuesto2;
                        // return Impuesto2;
                        //return Salario_impuesto;


                        //Si el Salario es menor que el monto 3
                        if (Salario_impuesto2 < monto3)
                        {
                            Impuesto3 = Salario_impuesto2 * porcentaje3;
                            Impuesto = Impuesto + Impuesto3;
                            return Impuesto;

                        }
                        else if (Salario_impuesto2 > monto3)
                        {
                            Impuesto3 = monto3 * porcentaje3;
                            Salario_impuesto3 = Salario_impuesto2 - monto3;
                            Impuesto = Impuesto + Impuesto3;
                            //return Impuesto3;
                            //return Salario_impuesto;





                            //Si el Salario es menor que el monto 4
                            //Hasta antes de acá,si calcula la renta
                            if (Salario_impuesto3 < monto4)
                            {
                                Impuesto4 = Salario_impuesto3 * porcentaje4;
                                Impuesto = Impuesto + Impuesto4;
                                return Impuesto;

                            }
                            else if (Salario_impuesto3 > monto4)
                            {
                                Impuesto4 = monto * porcentaje4;
                                Impuesto = Impuesto + Impuesto4;
                                // return Impuesto4;
                                // return Salario_impuesto;

                            }
                            


                        }
                    }
                }

            }

            return Impuesto;


        }


        //Permite realizar las deducciones de la CCSS
        double IGestorPlanilla.DeducSeguro(double salarioBase)
        {
            double deducCCSS = 0;
            deducCCSS = salarioBase * 0.1067;
            return deducCCSS;
        }


        //Permite calcular el salario final
        double IGestorPlanilla.CalSalarioFinal(double DeducAusencias, double DeducRenta, double DeducSeguro)
        {
            double salario = 0;
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


//Se tiene un formulario solo con la cedula, el nombre y el salario
//Se tiene un boton de calcular planilla
//Al dar click al boton, el sistema ejecuta el calculo de la planilla segun cada salario
//El sistema guarda los registros **se requiere un ciclo
//Se muestra un reporte para descargar en pdf

//Las deducciones se manejan individual por empleado




/*//Variables a utilizar
            double exento = 941000.00;
            double monto1 = 440000;
            double monto2 = 1042000;
            double monto3 = 2422000;
            double monto4 = 95154999;
            double porcentaje1 = 0.10;
            double porcentaje2 = 0.15;
            double porcentaje3 = 0.20;
            double porcentaje4 = 0.25;
            double monto = 0.0;
            double Impuesto = 0.0;
            double Impuesto1 = 0.0;
            double Impuesto2 = 0.0;
            double Impuesto3 = 0.0;
            double Impuesto4 = 0.0;



            //Salario luego de quitar el exento de 941000.00
            Salario_impuesto = salarioBase - exento;



            //Si el salario restante  es menor al monto 1 de 440000
            if (Salario_impuesto < monto1)
            {
                //Se calcula el impuesto sobre el salario restante
                Impuesto1 = Salario_impuesto * porcentaje1;
                Impuesto = Impuesto + Impuesto1;
            }

            //Si el salario restante es menor al monto 2 de 1042000
            else if (Salario_impuesto < monto2)
            {
                //Se calcula el impuesto sobre el salario restante (ya se hizo el rebajo del primer porcentaje)
                Impuesto2 = Salario_impuesto * porcentaje2;
                Impuesto = Impuesto + Impuesto2;
            }
            
            else if (Salario_impuesto < monto3)
            {
                Impuesto3 = Salario_impuesto * porcentaje3;
                Impuesto = Impuesto + Impuesto3;
            }

            else if (Salario_impuesto < monto4)
            {
                Impuesto4 = Salario_impuesto * porcentaje4;
                Impuesto = Impuesto + Impuesto4;
            }


            //monto = 1381000 - 941000;
            if (monto == 440000){

                Impuesto1 = monto * porcentaje1;
                return (int)Impuesto1;
            }

            Impuesto = Impuesto + Impuesto1;




            //monto = 2423000 - 1381000;
            if (monto == 1042000)
            {
                Impuesto2 = monto * 0.15;
                return (int)Impuesto2;

            }
            Impuesto = Impuesto + Impuesto2;




            //monto = 4845000 - 2423000;
            if (monto == 2422000)
            {
                Impuesto3 = monto * 0.20;
                return (int)Impuesto3;

            }

            Impuesto = Impuesto + Impuesto3;  


            //monto = 99999999 - 4845000 ;
            if (monto == 95154999)
            {
                Impuesto4 = monto * 0.25;
                return (int)Impuesto4;

            }
            Impuesto = Impuesto + Impuesto4;





            return (int)Impuesto;*/

