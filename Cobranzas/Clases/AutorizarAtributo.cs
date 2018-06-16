
using Cobranzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cobranzas.Clases
{
	public class AutorizarAtributo : AuthorizeAttribute
	{
		private List<int> PermisosRequeridos { get; set; }

		public AutorizarAtributo()
		{
			this.PermisosRequeridos = new List<int>();
		}

		public AutorizarAtributo(params int[] Permisos)
		{
			this.PermisosRequeridos = new List<int>();
			this.PermisosRequeridos = Permisos.ToList();
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (Util.Usuario != null)
			{
				UsuarioSesion newUser = new UsuarioSesion(Util.Usuario.Login)
				{
					Usuario = Util.Usuario
				};
				HttpContext.Current.User = newUser;
			}
			base.OnAuthorization(filterContext);
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			try
			{
				bool authorized = base.AuthorizeCore(httpContext);
				var routeData = httpContext.Request.RequestContext.RouteData;
				var controller = routeData.GetRequiredString("controller");
				var action = routeData.GetRequiredString("action");

				if (Util.Usuario != null)
				{
					UsuarioSesion userLoged = httpContext.User as UsuarioSesion;
					if (PermisosRequeridos.Count > 0)
					{
						return userLoged.IsInRole(PermisosRequeridos.Select(x => x.ToString()).ToArray());
					}
					else
					{
						// If doesn't have Required Roles, allow all
						return true;
					}
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			int ErrorCode = 0;
			if (filterContext.HttpContext.Items["ErrorCode"] != null)
			{
				ErrorCode = Convert.ToInt32(filterContext.HttpContext.Items["ErrorCode"].ToString());
			}

			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.HttpContext.Response.StatusCode = 403;
			}
			else
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Cuenta" }, { "action", "CerrarSesion" } });
				//base.HandleUnauthorizedRequest(filterContext);
			}
		}
	}
}