//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoportePatitosBD.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Evaluacion
    {
        [Display(Name = "ID")]
        public int ID_evaluacion { get; set; }
        [Display(Name = "C�dula")]
        public int Cedula { get; set; }
        [Display(Name = "Pregunta 1")]
        public int Pregunta_1 { get; set; }
        [Display(Name = "Pregunta 2")]
        public int Pregunta_2 { get; set; }
        [Display(Name = "Pregunta 3")]
        public int Pregunta_3 { get; set; }
        [Display(Name = "Pregunta 4")]
        public int Pregunta_4 { get; set; }
        [Display(Name = "Pregunta 5")]
        public int Pregunta_5 { get; set; }
    
        public virtual Empleado Empleado { get; set; }
    }
}
