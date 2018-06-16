using Cobranzas.Data;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using Cobranzas.Clases;

namespace Cobranzas.Controllers
{
	public class dispositivoesController : Controller
	{
		private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();
		List<KeyValuePair<string, string>> mensajes = new List<KeyValuePair<string, string>>();

		// GET: dispositivoes
		public ActionResult Index()
		{
			return View();
		}
		public async Task<ActionResult> List()
		{
			return PartialView("_List",await db.dispositivoes.ToListAsync());
		}
		// GET: dispositivoes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			dispositivo dispositivo = await db.dispositivoes.FindAsync(id);
			if (dispositivo == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Details",dispositivo);
		}

		// GET: dispositivoes/Create
		public ActionResult Create()
		{
			return PartialView("_Create", new dispositivo());
		}

		// POST: dispositivoes/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "modelo,imei,dispositivoID")] dispositivo dispositivo)
		{
			if (ModelState.IsValid)
			{
				db.dispositivoes.Add(dispositivo);
				await db.SaveChangesAsync();
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensajes);
			}

			return PartialView("_Create",dispositivo);
		}

		// GET: dispositivoes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			dispositivo dispositivo = await db.dispositivoes.FindAsync(id);
			if (dispositivo == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Edit",dispositivo);
		}

		// POST: dispositivoes/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "modelo,imei,dispositivoID")] dispositivo dispositivo)
		{
			if (ModelState.IsValid)
			{
				db.Entry(dispositivo).State = EntityState.Modified;
				await db.SaveChangesAsync();
				mensajes.Clear();
				mensajes.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensajes);
			}
			return View(dispositivo);
		}

		
		// POST: dispositivoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id)
		{
			dispositivo dispositivo = await db.dispositivoes.FindAsync(id);
			db.dispositivoes.Remove(dispositivo);
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
