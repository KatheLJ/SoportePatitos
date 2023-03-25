using SoportePatitosBD.Interface;
using SoportePatitosBD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoportePatitosBD.Repositorios
{
    internal class GestorPlanilla : IGestorPlanilla
    {

        int IGestorPlanilla.ParamPlanilla()
        {

            int n = 0;
            using (SoportePatitosEntities ContextoBD = new SoportePatitosEntities())
            {
                ContextoBD.spPLanillla();
                n = ContextoBD.SaveChanges();
            }
            return n;

        }


    }
}
