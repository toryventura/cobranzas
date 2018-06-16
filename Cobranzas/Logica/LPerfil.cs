
using Cobranzas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cobranzas.Logica
{
	public class LPerfil
	{
		public static Perfil Obtener(long ID)
		{
			try
			{
				using (var BD = new Data.DB_A3C345_cobranzasEntities())
				{
					var z = (from x in BD.Perfils where x.PerfilID == ID select x).FirstOrDefault();
					if (z != null)
					{
						return LPerfil.DataToEntidad(z, false);
					}
				}
				throw new Exception("El perfil no existe");
			}
			catch (Exception ex)
			{
				throw new Exception("Logica obtner", ex);
			}
		}
		public static List<Perfil> allPerfils()
		{
			try
			{
				using (var BD = new Data.DB_A3C345_cobranzasEntities())
				{
					var list = new List<Perfil>();
					var z = BD.Perfils.ToList();
					if (z != null)
					{
						foreach (var item in z)
						{
							list.Add(LPerfil.DataToEntidad(item, false));
						}
						return list;
					}
				}
				throw new Exception("El perfil no existe");
			}
			catch (Exception ex)
			{
				throw new Exception("Logica obtner", ex);
			}
		}

		public static List<Perfil> BuscarPerfiles(string nombre, int estado, bool cargarpermisos = false)
		{
			//Estado: 0 = todos, 1 = habilitados, 2 = deshabilitados
			try
			{
				List<Perfil> rtn = new List<Perfil>();
				using (var BD = new Data.DB_A3C345_cobranzasEntities())
				{
					var z = (from x in BD.Perfils
							 where
								 (x.PerfilNombre.ToUpper().Contains(nombre.ToUpper()) || string.IsNullOrEmpty(nombre))
								 &&
								 (
									 (x.PerfilHabilitado == true && estado == 1)
									 || (x.PerfilHabilitado == false && estado == 2)
									 || estado == 0
								 )
							 select x).ToList();
					foreach (var p in z)
					{
						rtn.Add(DataToEntidad(p, !cargarpermisos));
					}
				}
				return rtn;
			}
			catch (Exception ex)
			{
				throw new Exception("Logica BuscarPerfiles ", ex);
			}
		}

		public static long Guardar(Perfil Entidad)
		{
			try
			{
				using (var BD = new Data.DB_A3C345_cobranzasEntities())
				{
					if (Entidad.ID > 0)
					{
						//editar
						if (BD.Perfils.Count(x => x.PerfilNombre.ToUpper().Equals(Entidad.Nombre.ToUpper().Trim()) && x.PerfilID != Entidad.ID) > 0)
						{
							throw new Exception("El nombre del perfil ya se encuentra registrado en el sistema");
						}
						var p = BD.Perfils.FirstOrDefault(x => x.PerfilID == Entidad.ID);
						if (p == null)
						{
							throw new Exception("El perfil no existe");
						}
						p.PerfilDescripcion = Entidad.Descripcion;
						p.PerfilHabilitado = Entidad.Habilitado;
						p.PerfilNombre = Entidad.Nombre;
						p.Permisoes.Clear();
						foreach (var m in Entidad.Permisos)
						{
							var per = BD.Permisoes.FirstOrDefault(x => x.PermisoID == m.ID);
							if (per != null)
							{
								if (per.PermisoPadreID.HasValue)
								{
									var padre = per.Permiso2;
									LPerfil.BucleAgregarPadre(ref p, ref padre);
								}
								if (!p.Permisoes.Any(x => x.PermisoID == per.PermisoID))
								{
									p.Permisoes.Add(per);
								}

							}
						}
						BD.SaveChanges();
						return p.PerfilID;
					}
					else
					{
						//nuevo
						if (BD.Perfils.Count(x => x.PerfilNombre.ToUpper().Equals(Entidad.Nombre.ToUpper().Trim())) > 0)
						{
							throw new Exception("El nombre del perfil ya se encuentra registrado en el sistema");
						}
						Data.Perfil p = new Data.Perfil()
						   {
							   PerfilDescripcion = Entidad.Descripcion,
							   PerfilHabilitado = Entidad.Habilitado,
							   PerfilNombre = Entidad.Nombre
						   };
						p.Permisoes = new List<Data.Permiso>();
						foreach (var m in Entidad.Permisos)
						{
							var per = BD.Permisoes.FirstOrDefault(x => x.PermisoID == m.ID);
							if (per != null)
							{
								if (per.PermisoPadreID.HasValue)
								{
									var padre = per.Permiso2;
									LPerfil.BucleAgregarPadre(ref p, ref padre);
								}
								if (!p.Permisoes.Any(x => x.PermisoID == per.PermisoID))
								{
									p.Permisoes.Add(per);
								}

							}
						}
						BD.Perfils.Add(p);
						BD.SaveChanges();
						return p.PerfilID;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Logica guardar", ex);
			}
		}

		private static void BucleAgregarPadre(ref Data.Perfil perfil, ref Data.Permiso permiso)
		{
			var perID = permiso.PermisoID;
			var padre = permiso.Permiso2;
			if (permiso.PermisoPadreID.HasValue)
			{
				LPerfil.BucleAgregarPadre(ref perfil, ref padre);
			}
			if (!perfil.Permisoes.Any(x => x.PermisoID == perID))
			{
				perfil.Permisoes.Add(permiso);
			}
		}

		public static bool CambiarEstado(long ID, bool estado)
		{
			try
			{
				using (var BD = new Data.DB_A3C345_cobranzasEntities())
				{
					var g = (from x in BD.Perfils where x.PerfilID == ID select x).FirstOrDefault();
					if (g == null)
					{
						throw new Exception("El perfil no existe");
					}
					g.PerfilHabilitado = estado;
					BD.SaveChanges();
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Logica caombiarestado", ex);
			}
		}

		public static Perfil DataToEntidad(Data.Perfil d, bool EsListado = true)
		{
			var permisos = new List<Permiso>();
			if (!EsListado)
			{
				foreach (var p in d.Permisoes)
				{
					permisos.Add(LPermiso.DataToEntidad(p, false));
				}
			}

			return new Entidades.Perfil()
			{
				Descripcion = d.PerfilDescripcion,
				Habilitado = d.PerfilHabilitado.Value,
				ID = d.PerfilID,
				Nombre = d.PerfilNombre,
				Permisos = permisos,
				TodosPermisos = null
			};
		}
	}
}
