using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cobranzas.Entidades
{
	public class Permiso
	{
		public int ID { get; set; }
		public string Nombre { get; set; }
		public Nullable<int> PadreID { get; set; }
		public Nullable<int> Orden { get; set; }
		public Nullable<int> MenuID { get; set; }

		public virtual Menu Menu { get; set; }

		#region ParaPantalleo
		public bool Checked { get; set; }
		public bool EnPerfil { get; set; }
		public virtual List<Permiso> Hijos { get; set; }
		#endregion

		public object Clone()
		{
			return Clonadora.Clonar(this);
		}

		public Permiso Clonar()
		{
			return (Permiso)this.Clone();
		}
	}
}
