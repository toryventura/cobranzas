using Cobranzas.Clases;
using Cobranzas.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Cobranzas.Controllers
{
	public class CuentaController : Controller
	{

		private Usuario UsuarioTemp
		{
			get
			{
				if (Session["LoginUsuario"] != null)
				{
					return (Usuario)Session["LoginUsuario"];
				}
				else
				{
					return null;
				}
			}
			set
			{
				if (value == null)
				{
					Session.Remove("LoginUsuario");
				}
				else
				{
					Session["LoginUsuario"] = value;
				}
			}
		}
		// GET: Cuenta
		public ActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		public ActionResult IniciarSesion()
		{
			return PartialView();
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult IniciarSesion(string usuario = "", string contrasena = "")
		{
			var mensajes = new List<KeyValuePair<string, string>>();
			try
			{
				if (Validaciones.EsNulaVacia(usuario) || Validaciones.EsNulaVacia(contrasena))
				{
					mensajes.Add(Util.mensaje("usuario, contrasena", "Usuario y/o contraseña inválida", false));
				}
				if (mensajes.Count > 0)
				{
					return Json(mensajes);
				}

				var entidadUsuario = Logica.LUsuario.IniciarSesion(usuario, SEGURIDAD.encriptarMD5(contrasena));
				//var entidadUsuario = ServicioCuenta.IniciarSesion(usuario, SEGURIDAD.encriptarMD5(contrasena), Util.IPCliente(TipoIP.V4));
				//entidadUsuario.Contrasena = SEGURIDAD.encriptarMD5(contrasena);
				if (entidadUsuario.CambiarContrasena)
				{
					this.UsuarioTemp = entidadUsuario;
					mensajes.Clear();
					mensajes.Add(Util.mensaje("JS", "MostrarCambiarContrasena();"));
					return Json(mensajes);
				}
				else
				{
					Util.Usuario = entidadUsuario;
					this.UsuarioTemp = null;
					mensajes.Clear();

					//mensajes.Add(Util.mensaje("JS", string.Format("RedireccionarOcultandoFormulario('{0}');", Url.Action("Index", "Home"))));
					var urls = Url.Action("Index", "Home");// "/Home/Index";

					//var re = String.Format("RedireccionarOcultandoFormulario('{0}')", urls);
					mensajes.Add(Util.mensaje("JS", urls, false));
					//mensajes.Add(Util.mensaje("JS", string.Format("RedireccionarOcultandoFormulario('{0}');", Url.Action("Index", "Home"))));
					return Json(mensajes);
				}
			}
			catch (Exception ex)
			{
				mensajes.Clear();
				mensajes.Add(Util.mensaje("ERROR", ex.Message));
				return Json(mensajes);
			}
		}
		[AllowAnonymous]
		public ActionResult CerrarSesion()
		{
			Session.Clear();
			return RedirectToAction("IniciarSesion", "Cuenta");
		}
	}
}