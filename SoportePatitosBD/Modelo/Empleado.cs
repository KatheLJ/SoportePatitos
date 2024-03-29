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

    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Evaluacion = new HashSet<Evaluacion>();
            this.Asistencia = new HashSet<Asistencia>();
            this.Planilla = new HashSet<Planilla>();
        }

        [Display(Name = "Cédula")]
        public int Cedula { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre_Empleado { get; set; }
        [Display(Name = "Fecha ingreso")]
        public System.DateTime Fecha_ingreso { get; set; }
        public int ID_departamento { get; set; }
        public int ID_perfil { get; set; }
        public int ID_puesto { get; set; }
        public int ID_horario { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        [Display(Name = "Hijos")]
        public int Cantidad_Hijos { get; set; }
        public int ID_Estado_Civil { get; set; }
    
        public virtual Horario Horario { get; set; }
        public virtual Perfil Perfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asistencia> Asistencia { get; set; }
        public virtual Estado_Civil Estado_Civil { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Puesto Puesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Planilla> Planilla { get; set; }
    }
}
