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
    
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.Permisoes = new HashSet<Permiso>();
        }
    
        public string Texto { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public Nullable<int> MenuPadre { get; set; }
        public Nullable<int> Orden { get; set; }
        public Nullable<bool> EsGlobal { get; set; }
        public int MenuID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permiso> Permisoes { get; set; }
    }
}