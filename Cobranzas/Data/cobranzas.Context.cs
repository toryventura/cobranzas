﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cobranzas.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A3C345_cobranzasEntities : DbContext
    {
        public DB_A3C345_cobranzasEntities()
            : base("name=DB_A3C345_cobranzasEntities")
        {
			this.Configuration.ProxyCreationEnabled = false;
		}
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AsignacionCliente> AsignacionClientes { get; set; }
        public virtual DbSet<cliente> clientes { get; set; }
        public virtual DbSet<controlPago> controlPagoes { get; set; }
        public virtual DbSet<cuota> cuotas { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }
        public virtual DbSet<dispositivo> dispositivoes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NotaVenta> NotaVentas { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Permiso> Permisoes { get; set; }
        public virtual DbSet<planPago> planPagoes { get; set; }
        public virtual DbSet<producto> productoes { get; set; }
        public virtual DbSet<punto> puntos { get; set; }
        public virtual DbSet<Seguimiento> Seguimientoes { get; set; }
        public virtual DbSet<ubicacionCobro> ubicacionCobroes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}