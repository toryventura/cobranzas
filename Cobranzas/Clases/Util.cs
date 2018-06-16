
using Cobranzas.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cobranzas.Clases
{
	public class Util
	{
		public static string OK = "OK";
		public static string OKMENSAJE = "SE HA REALIZADO LA OPERACION CORRECTAMENTE";
		public static string ERROR = "ERROR";
		public static string ERRORMENSAJE = "NO SE REALIAZO LA OPERACION";
		public static Usuario Usuario
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Session != null)
					{
						if (HttpContext.Current.Session["UsuarioSesion"] != null)
						{
							return (Usuario)HttpContext.Current.Session["UsuarioSesion"];
						}
					}
				}
				return null;
			}
			set
			{
				HttpContext.Current.Session["UsuarioSesion"] = value;
			}
		}
		public static Perfil perfil
		{
			get
			{
				if (Usuario.Perfiles.Count > 0)
					return Usuario.Perfiles.First();
				else
					return null;
			}
		}

		public static string IPCliente(TipoIP ipTipo)
		{
			try
			{
				//return HttpContext.Current.Request.UserHostName;
				if (ipTipo == TipoIP.V4)
				{
					string IP4Address = String.Empty;

					foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
					{
						if (IPA.AddressFamily.ToString() == "InterNetwork")
						{
							IP4Address = IPA.ToString();
							break;
						}
					}

					if (IP4Address != String.Empty)
					{
						return IP4Address;
					}

					foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
					{
						if (IPA.AddressFamily.ToString() == "InterNetwork")
						{
							IP4Address = IPA.ToString();
							break;
						}
					}

					return IP4Address;
				}
				if (ipTipo == TipoIP.V6)
				{
					System.Web.HttpContext context = System.Web.HttpContext.Current;
					string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

					if (!string.IsNullOrEmpty(ipAddress))
					{
						string[] addresses = ipAddress.Split(',');
						if (addresses.Length != 0)
						{
							return addresses[0];
						}
					}

					return context.Request.ServerVariables["REMOTE_ADDR"];
				}
				return "";
			}
			catch
			{
				return "";
			}
		}

		public static string ContrasenaPorDefecto
		{
			get
			{
				try
				{
					string contra = WebConfigurationManager.AppSettings["ContrasenaDefecto"];
					return contra;
				}
				catch
				{
					return "Abc_12345678";
				}
			}
		}

		public static HandleErrorInfo Error(Exception ex)
		{
			string ActualController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
			string ActualAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
			return new HandleErrorInfo(ex, ActualController, ActualAction);
		}

		public static KeyValuePair<string, string> mensaje(string key, string value, bool reemplazarEspeciales = true)
		{
			if (!reemplazarEspeciales)
			{
				return new KeyValuePair<string, string>(key, value);
			}
			else
			{
				return new KeyValuePair<string, string>(key, value.Replace("\"", "").Replace("\\", "").Replace("'", ""));
			}
		}

		public static IEnumerable<SelectListItem> Meses
		{
			get
			{
				var cInfo = new CultureInfo("es-US");
				TextInfo tInfo = cInfo.TextInfo;

				return cInfo.DateTimeFormat.MonthNames.Select((monthName, index) => new SelectListItem
				{
					Value = (index + 1).ToString(),
					Text = tInfo.ToTitleCase(monthName)
				});
				//return DateTimeFormatInfo
				//       .InvariantInfo
				//       .MonthNames
				//       .Select((monthName, index) => new SelectListItem
				//       {
				//           Value = (index + 1).ToString(),
				//           Text = monthName
				//       });
			}
		}

		public static HtmlString ConstruirFuncionesDisponibles(HtmlHelper Html, string etiquetaName, List<Permiso> TodosPermisos, string IDListaFunciones, List<Permiso> PermisosValidar, bool deshabilitarTodo = false)
		{

			System.Text.StringBuilder retorno = new System.Text.StringBuilder();

			int contadorAgujasDesplegables = 0;
			int indice = -1;

			if (TodosPermisos.Count > 0)
			{
				retorno.Append("<ul id='" + IDListaFunciones + "'>");
				foreach (var p in TodosPermisos)
				{
					retorno.Append(ConstruirFuncionesDisponiblesItem(p, ref contadorAgujasDesplegables, etiquetaName, ref indice, ref Html, ref deshabilitarTodo, ref PermisosValidar));
				}
				retorno.Append("</ul>");
			}
			return new HtmlString(retorno.ToString());
		}

		public static string ConstruirFuncionesDisponiblesItem(Permiso p, ref int contadorAgujasDesplegables, string etiquetaName, ref int indice, ref HtmlHelper Html, ref bool deshabilitarTodo, ref List<Permiso> PermisosValidar)
		{
			indice++;
			contadorAgujasDesplegables++;
			System.Text.StringBuilder retorno = new System.Text.StringBuilder();
			retorno.Append("<li>");
			p.EnPerfil = PermisosValidar.Any(x => x.ID == p.ID && x.EnPerfil == true);
			p.Checked = PermisosValidar.Any(x => x.ID == p.ID);
			Dictionary<string, object> AtributosHTMLCheck = new Dictionary<string, object>();
			AtributosHTMLCheck.Add("class", "check");
			AtributosHTMLCheck.Add("id", string.Format("FuncionDisponible_{0}", p.ID));
			if (p.EnPerfil)
			{
				p.Checked = true;
				AtributosHTMLCheck.Add("disabled", "disabled");
			}
			if (p.Checked)
			{
				//AtributosHTMLCheck.Add("checked", "checked");
			}
			if (deshabilitarTodo)
			{
				if (!AtributosHTMLCheck.Any(x => x.Key.ToUpper().Equals("DISABLED")))
				{
					AtributosHTMLCheck.Add("disabled", "disabled");
				}
			}
			if (p.Hijos != null)
			{
				if (p.Hijos.Count > 0)
				{
					retorno.AppendFormat("<input type='checkbox' class='desplegador' id='aguja_desplegable_{0}'>", contadorAgujasDesplegables);
					retorno.AppendFormat("<label for='aguja_desplegable_{0}'></label>", contadorAgujasDesplegables);
				}
			}
			retorno.Append(Html.Hidden(etiquetaName + ".Index", indice));
			retorno.Append(Html.Hidden(etiquetaName + "[" + indice + "].ID", p.ID));
			retorno.Append(Html.CheckBox(etiquetaName + "[" + indice + "].Checked", p.Checked, AtributosHTMLCheck));
			retorno.AppendFormat("<label for='FuncionDisponible_{0}'>{1}</label>", p.ID, p.Nombre);
			//retorno.AppendFormat("<label for='FuncionDisponible_{0}'>{1} - {2}</label>", p.ID, p.Nombre, p.Checked);

			if (p.Hijos != null)
			{
				if (p.Hijos.Count > 0)
				{

					retorno.Append("<ul>");
					foreach (var ph in p.Hijos)
					{
						retorno.Append(ConstruirFuncionesDisponiblesItem(ph, ref contadorAgujasDesplegables, etiquetaName, ref indice, ref Html, ref deshabilitarTodo, ref PermisosValidar));
					}
					retorno.Append("</ul>");
				}
			}
			retorno.Append("</li>");
			return retorno.ToString();
		}
	}
}