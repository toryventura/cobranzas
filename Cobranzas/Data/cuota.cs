//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class cuota
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cuota()
        {
            this.controlPagoes = new HashSet<controlPago>();
        }
    
        public string descripcion { get; set; }
        public Nullable<double> debe { get; set; }
        public Nullable<double> haber { get; set; }
        public Nullable<double> saldo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public int cuotasID { get; set; }
        public Nullable<int> planPagoID { get; set; }
        public Nullable<int> pagado { get; set; }
        public Nullable<System.DateTime> fechapagado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<controlPago> controlPagoes { get; set; }
        public virtual planPago planPago { get; set; }
    }
}