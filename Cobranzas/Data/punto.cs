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
    
    public partial class punto
    {
        public Nullable<double> latitud { get; set; }
        public Nullable<double> longitud { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public int puntosID { get; set; }
        public Nullable<int> dispositivoID { get; set; }
    
        public virtual dispositivo dispositivo { get; set; }
    }
}
