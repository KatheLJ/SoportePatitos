﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SoportePatitosEntities : DbContext
    {
        public SoportePatitosEntities()
            : base("name=SoportePatitosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<Estado_Civil> Estado_Civil { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Planilla> Planilla { get; set; }
    
        public virtual ObjectResult<spEvaluacionEmpleado_Result> spEvaluacionEmpleado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spEvaluacionEmpleado_Result>("spEvaluacionEmpleado");
        }
    
        public virtual ObjectResult<spPLanillla_Result> spPLanillla()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spPLanillla_Result>("spPLanillla");
        }
    }
}
