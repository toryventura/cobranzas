using Cobranzas.Clases;
using Cobranzas.Entidades;
using Cobranzas.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cobranzas.Controllers
{
	public class UsuarioController : Controller
	{
		// GET: Usuario
		public ActionResult Index()
		{
			return View();
		}

		// GET: CompromisoPago/Details/5
		public ActionResult Details(int id)
		{
			var list = LUsuario.toListaUsuario();
			var obj = list.Where(x => x.ID == id).FirstOrDefault();
			if (obj != null)
			{
				return PartialView("_Details", obj);
			}
			return View();
		}

		// GET: CompromisoPago/Create
		public ActionResult Create()
		{
			
			var obj = new Entidades.Usuario();

			var perfils = LPerfil.allPerfils();
			ViewBag.IdPerfil = new SelectList(perfils, "ID", "Nombre");
			return PartialView("_Create", obj);
		}
		public ActionResult Edit(int id)
		{
			
			var list = LUsuario.toListaUsuario();
			var obj = list.Where(x => x.ID == id).FirstOrDefault();
			
			
			if (obj != null)
			{
				return PartialView("_Edit", obj);
			}
			return View();

		}
		[HttpPost]
		public ActionResult Lists()
		{
			var list = LUsuario.toListaUsuario();
			return PartialView("_List", list);
		}
		// POST: CompromisoPago/Create
		[HttpPost]
		public ActionResult Create(Entidades.Usuario collection)
		{
			var mensajes = new List<KeyValuePair<string, string>>();
			try
			{
				// TODO: Add insert logic here

				collection.Habilitado = true;
				collection.Contrasena = SEGURIDAD.encriptarMD5(collection.Contrasena);
				var sw = LUsuario.add(collection, collection.IdPerfil, collection.IdFase);
				if (sw)
				{
					mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				}
				else
				{
					mensajes.Add(Util.mensaje(Util.ERROR, Util.ERRORMENSAJE));
				}
				return Json(mensajes);

			}
			catch (Exception ex)
			{
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.ERROR, ex.Message));
				return Json(mensajes);
			}
		}

		// GET: CompromisoPago/Edit/5


		// POST: CompromisoPago/Edit/5
		[HttpPost]
		public ActionResult Edit(Entidades.Usuario collection)
		{
			var mensajes = new List<KeyValuePair<string, string>>();
			try
			{
				var sw = LUsuario.update(collection);
				if (sw)
				{

					mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				}
				else
				{
					mensajes.Add(Util.mensaje(Util.ERROR, Util.ERRORMENSAJE));
				}
				return Json(mensajes);
			}
			catch (Exception ex)
			{
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.ERROR, ex.Message));
				return Json(mensajes);
			}
		}

		// GET: CompromisoPago/Delete/5
		[HttpPost]
		public ActionResult Delete(int id)
		{
			var mensajes = new List<KeyValuePair<string, string>>();
			try
			{
				var list = LUsuario.toListaUsuario();
				var obj = list.Where(x => x.ID == id).FirstOrDefault();
				if (obj != null)
				{
					var sw = LUsuario.delet(obj);
					if (sw)
					{
						mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
					}
					else
					{
						mensajes.Add(Util.mensaje(Util.ERROR, Util.ERRORMENSAJE));
					}
				}
				else
					mensajes.Add(Util.mensaje(Util.ERROR, Util.ERRORMENSAJE));

				return Json(mensajes);


			}
			catch (Exception ex)
			{

				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.ERROR, ex.Message));
				return Json(mensajes);
			}

		}
	}
}
