using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobranzas.Entidades
{
	public class Perfil
	{
		public long ID { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public bool Habilitado { get; set; }
		public virtual List<Permiso> Permisos { get; set; }

		#region ParaPantalla
		public virtual List<Permiso> TodosPermisos { get; set; }
		public bool Checked { get; set; }

		#endregion
	}
}
