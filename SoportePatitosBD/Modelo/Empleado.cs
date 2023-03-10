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
    
    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Asistencia = new HashSet<Asistencia>();
            this.Evaluacion = new HashSet<Evaluacion>();
        }
    
        public int Cedula { get; set; }
        public string Nombre_Empleado { get; set; }
        public System.DateTime Fecha_ingreso { get; set; }
        public int ID_departamento { get; set; }
        public int ID_perfil { get; set; }
        public int ID_puesto { get; set; }
        public int ID_horario { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asistencia> Asistencia { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Horario Horario { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Puesto Puesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
