using System;
using System.Collections.Generic;
using System.Linq;

namespace Cobranzas.Entidades
{
	[Serializable]
	public class Usuario : ICloneable
	{
		public long ID { get; set; }
		public string Nombre { get; set; }
		public string Apellido1 { get; set; }
		public string Apellido2 { get; set; }
		public string Email { get; set; }
		public string Telefono { get; set; }
		public string Login { get; set; }
		public string Contrasena { get; set; }
		public bool Habilitado { get; set; }
		public bool EsSuperAdmin { get; set; }
		public bool CambiarContrasena { get; set; }
		public int IdFase { get; set; }
		public int IdPerfil { get; set; }

		public virtual List<Perfil> Perfiles { get; set; }
		public virtual List<Permiso> Permisos { get; set; }
		//public virtual List<UsuarioIPRestringida> IPsRestringidas { get; set; }

		#region paraPantalla

		public string NombreCompleto
		{
			get
			{
				try
				{
					return string.Concat(this.Nombre, " ", this.Apellidos);
				}
				catch
				{
					return "";
				}
			}
		}

		public string Apellidos
		{
			get
			{
				try
				{
					return string.Concat(this.Apellido1, Validaciones.EsNulaVacia(this.Apellido2) ? "" : " ", this.Apellido2);
				}
				catch
				{
					return "";
				}
			}
		}

		//todos los permisos asignados al usuario
		public List<Permiso> TodosLosPermisos
		{
			get
			{
				try
				{
					var resultado = new List<Permiso>();
					foreach (var perfil in this.Perfiles)
					{
						foreach (var p in perfil.Permisos)
						{
							if (!resultado.Any(y => y.ID == p.ID))
							{
								p.Checked = true;
								p.EnPerfil = true;
								resultado.Add(p);
							}
						}
					}
					foreach (var p in this.Permisos)
					{
						if (!resultado.Any(y => y.ID == p.ID))
						{
							p.Checked = true;
							p.EnPerfil = false;
							resultado.Add(p);
						}
					}
					return resultado;
				}
				catch
				{
					return new List<Permiso>();
				}
			}
		}
		public virtual List<Permiso> TodosPermisosBD { get; set; }
		public List<Permiso> TodosPermisosBDSinHijos
		{
			get
			{
				try
				{
					var rtn = new List<Permiso>();
					if (this.TodosPermisosBD != null)
					{
						foreach (var p in this.TodosPermisosBD)
						{
							rtn.Add(p);
							if (p.Hijos != null)
							{
								rtn.AddRange(SacarHijos(p));
							}
						}
					}
					return rtn;
				}
				catch
				{
					return new List<Permiso>();
				}
			}
		}
		private List<Permiso> SacarHijos(Permiso p)
		{
			try
			{
				var rtn = new List<Permiso>();
				if (p.Hijos != null)
				{
					rtn.AddRange(p.Hijos);
					foreach (var h in p.Hijos)
					{
						if (h.Hijos != null)
						{
							rtn.AddRange(SacarHijos(h));
						}
					}
				}
				return rtn;
			}
			catch
			{
				return new List<Permiso>();
			}
		}

		public virtual List<Perfil> TodosPerfilesBD { get; set; }

		public List<Perfil> PerfilesDisponibles
		{
			get
			{
				try
				{
					return this.TodosPerfilesBD.Where(x => this.Perfiles.Select(y => y.ID).ToList().Contains(x.ID) == false).ToList();
				}
				catch
				{
					return new List<Perfil>();
				}
			}
		}

		public List<Perfil> PerfilesAsignados
		{
			get
			{
				try
				{
					return this.TodosPerfilesBD.Where(x => this.Perfiles.Select(y => y.ID).ToList().Contains(x.ID) == true).ToList();
				}
				catch
				{
					return new List<Perfil>();
				}
			}
		}

		public List<Perfil> TodosPerfilesBDConChecked
		{
			get
			{
				try
				{
					this.TodosPerfilesBD.ForEach(x => x.Checked = this.PerfilesAsignados.Any(y => y.ID == x.ID));
					return this.TodosPerfilesBD;
				}
				catch
				{
					return new List<Perfil>();
				}
			}
		}

		#endregion

		public object Clone()
		{
			return Clonadora.Clonar(this);
		}

		public Usuario Clonar()
		{
			return (Usuario)this.Clone();
		}
	}
}
