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
    
    public partial class planPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public planPago()
        {
            this.cuotas = new HashSet<cuota>();
        }
    
        public Nullable<int> totalVenta { get; set; }
        public Nullable<int> ncuotas { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> monto { get; set; }
        public int planPagoID { get; set; }
        public Nullable<int> notaVentaID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cuota> cuotas { get; set; }
        public virtual NotaVenta NotaVenta { get; set; }
    }
}
