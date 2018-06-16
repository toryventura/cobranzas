using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobranzas.Entidades
{
	[Serializable]
	public class Menu
	{
		public int ID { get; set; }
		public string Texto { get; set; }
		public string Controlador { get; set; }
		public string Accion { get; set; }
		public Nullable<int> MenuPadre { get; set; }
		public int Orden { get; set; }
		public bool EsGlobal { get; set; }

		#region Pantalleo
		public virtual List<Menu> Hijos { get; set; }
		#endregion
	}
}
