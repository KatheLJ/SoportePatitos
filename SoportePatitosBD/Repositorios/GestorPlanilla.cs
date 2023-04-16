using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Repositorios
{
    public class GestorPlanilla : IGestorPlanilla
    {

        //Método para enlistar las planillas registradas en el sistema
        IEnumerable<Planilla> IGestorPlanilla.ListadoPlanilla()
        {
            //Se crea una lista de las planillas
            List<Planilla> Planillas = new List<Planilla>();
            //Se utiliza una conexión de Tipo SoportePatitosEntities
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                Planillas = ContextoBD.Planilla.ToList();
            }
            //Se regresa la lista
            return Planillas;
        }



        //Permite realizar la deducción de ausencias
        double IGestorPlanilla.DeducAusencias(double salarioBase) //, int cantidadDiasAusentes)
        {
            //Se crea una variable de días 
            int cantidadDiasAusentes = 2;
            //Las deducciones son el salario / 30 * ausencias
            double deducciones = (salarioBase/30) * cantidadDiasAusentes;
            //Se regresa la deducción
            return deducciones;
        }



        //Permite realizar las deducciones fiscales
        double IGestorPlanilla.DeducRenta(double salarioBase, int Cantidad_Hijos, int ID_Estado_Civil)
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

         
            
            double ReducHijos = 1750.00;
            double ReducCasado = 2650.00;
            double ReducFinalHijos = (double)Cantidad_Hijos * ReducHijos;




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
                    //Pero si es mayor que el monto 2
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
                            //El impuesto se calcula sobre el restante
                            Impuesto3 = Salario_impuesto2 * porcentaje3;
                            Impuesto = Impuesto + Impuesto3;
                            return Impuesto;

                        }
                        //Pero si es mayor que el monto 3
                        else if (Salario_impuesto2 > monto3)
                        {
                            //El impuesto se calcula sobre el monto 3
                            Impuesto3 = monto3 * porcentaje3;
                            Salario_impuesto3 = Salario_impuesto2 - monto3;
                            //El impuesto va a ser el impuesto que ya se tenga + el valor del impuesto 3
                            Impuesto = Impuesto + Impuesto3;
                            //return Impuesto3;
                            //return Salario_impuesto;


                            //Si el Salario es menor que el monto 4
                            if (Salario_impuesto3 < monto4)
                            {
                                //El impuesto se calcula sobre el restante
                                Impuesto4 = Salario_impuesto3 * porcentaje4;
                                Impuesto = Impuesto + Impuesto4;
                                return Impuesto;

                            }
                            //Pero si es mayor que el monto 3
                            else if (Salario_impuesto3 > monto4)
                            {
                                //El impuesto se calcula sobre el monto 4
                                Impuesto4 = monto * porcentaje4;
                                //El impuesto va a ser el impuesto que ya se tenga + el valor del impuesto 4
                                Impuesto = Impuesto + Impuesto4;
                                //Se regresa el impuesto
                                return Impuesto;
                                // return Salario_impuesto;

                            }
                            


                        }
                    }
                }


                //double ImpuestoFinal = 0.0;
                // ImpuestoFinal = Impuesto;

                //Permite realizar el rebajo de los hijos y el conyuge, al monto final del impuesto
                if (Cantidad_Hijos != 0 && ID_Estado_Civil == 1)
                {
                    Impuesto = Impuesto - ReducCasado - ReducFinalHijos;
                    //return ImpuestoFinal;

                }
                else if (Cantidad_Hijos != 0 && ID_Estado_Civil != 1)
                {

                    Impuesto = Impuesto - ReducFinalHijos;

                }
                else if (Cantidad_Hijos == 0 && ID_Estado_Civil == 1)
                {

                    Impuesto = Impuesto - ReducCasado;

                }
                else
                {

                }

            }

            

            return Impuesto;


        }


        //Permite realizar las deducciones de la CCSS
        double IGestorPlanilla.DeducSeguro(double salarioBase)
        {
            //Se crea un variable double 
            double deducCCSS = 0;
            //La variable va a ser igual al salario base * 0.1067
            deducCCSS = salarioBase * 0.1067;
            //Se regresan las deducciones por la CCSS
            return deducCCSS;
        }


        //Permite calcular el salario final
        double IGestorPlanilla.CalSalarioFinal(double salarioBase, double DeducAusencias, double DeducRenta, double DeducSeguro)
        {
           //La variable Salario base, va a ser igual al salario, menos la deducción por ausencias, por renta y por seguro
            salarioBase = salarioBase - DeducAusencias - DeducRenta - DeducSeguro;
            //Se regresa el salario base
            return salarioBase;
        }


        //Trae parametros de otras tablas, requeridos para la planilla, *NO se utiliza*
        int IGestorPlanilla.ParamPlanilla()
        {
            //Se crea una variable
            int n = 0;
            //Utiliza está conexión a la base de datos
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                //ContextoBD.spPLanillla();
                n = ContextoBD.SaveChanges();
            }
            return n;

        }

        //Método para crear un nuevo registro de tipo planilla (calcular la planilla de un empleado)
        int IGestorPlanilla.CrearPlanilla(Planilla pPlanilla)

        {
                //Se crea una variable
                int n = 0;
                //Utiliza está conexión a la base de datos
                using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
                {
                    //Añade un objeto de tipo Evaluación
                    ContextoBD.Planilla.Add(pPlanilla);
                    //Guarda los cambios
                    n = ContextoBD.SaveChanges();
                }
                //Regresa los datos en la variable
                return n;
            
        }



    }
}






