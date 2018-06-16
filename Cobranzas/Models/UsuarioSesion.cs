
using Cobranzas.Entidades;
using System.Linq;
using System.Security.Principal;

namespace Cobranzas.Models
{
	interface IPrincipalUser : IPrincipal
	{
		long ID { get; }
		string Nombres { get; }
		string Apellido1 { get; }
		string Apellido2 { get; }
		Usuario Usuario { get; set; }
		bool SuperAdmin { get; }
	}
	public class UsuarioSesion : IPrincipalUser
	{
		public IIdentity Identity { get; private set; }
		public bool IsInRole(string permisoID)
		{
			try
			{
				if (this.Usuario.EsSuperAdmin)
				{
					return true;
				}
				int permiso = int.Parse(permisoID);
				return this.Usuario.TodosLosPermisos.Any(x => x.ID == permiso);
			}
			catch
			{
				return false;
			}
		}

		public bool IsInRole(params string[] PermisosIDs)
		{
			try
			{
				if (this.Usuario.EsSuperAdmin)
				{
					return true;
				}
				if (this.Usuario.TodosLosPermisos.Any(x => PermisosIDs.Any(y => int.Parse(y) == x.ID)))
				{
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public UsuarioSesion(string login)
		{
			this.Identity = new GenericIdentity(login);
		}

		public long ID
		{
			get
			{
				return this.Usuario.ID;
			}
		}

		public string Nombres
		{
			get
			{
				return this.Usuario.Nombre;
			}
		}

		public string Apellido1
		{
			get
			{
				return this.Usuario.Apellido1;
			}
		}

		public string Apellido2
		{
			get
			{
				return this.Usuario.Apellido2;
			}
		}

		public string NombreCompleto
		{
			get
			{
				return this.Usuario.NombreCompleto;
			}
		}

		public Usuario Usuario { get; set; }
		public bool SuperAdmin
		{
			get
			{
				try
				{
					return this.Usuario.EsSuperAdmin;
				}
				catch
				{
					return false;
				}
			}
		}
	}
}