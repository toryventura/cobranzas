using Cobranzas.Clases;
using Cobranzas.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cobranzas.Controllers
{
	public class productoesController : Controller
	{
		private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();
		List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();

		// GET: productoes
		public ActionResult Index()
		{
			return View();
		}

		public async Task<ActionResult> List()
		{
			return PartialView("_List",await db.productoes.ToListAsync());
		}

		// GET: productoes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			producto producto = await db.productoes.FindAsync(id);
			if (producto == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Details",producto);
		}

		// GET: productoes/Create
		public ActionResult Create()
		{
			return PartialView("_Create",new producto());
		}

		// POST: productoes/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "nombre,precio,descripcion,productoID")] producto producto)
		{
			if (ModelState.IsValid)
			{
				db.productoes.Add(producto);
				await db.SaveChangesAsync();
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensajes);
			}

			return PartialView("_Create",producto);
		}

		// GET: productoes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			producto producto = await db.productoes.FindAsync(id);
			if (producto == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Edit",producto);
		}

		// POST: productoes/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "nombre,precio,descripcion,productoID")] producto producto)
		{
			if (ModelState.IsValid)
			{

				db.Entry(producto).State = EntityState.Modified;
				await db.SaveChangesAsync();
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensajes);
			}
			return PartialView("_Edit",producto);
		}

		// POST: productoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id)
		{
			producto producto = await db.productoes.FindAsync(id);
			db.productoes.Remove(producto);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
