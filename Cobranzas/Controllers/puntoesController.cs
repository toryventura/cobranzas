using Cobranzas.Data;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cobranzas.Controllers
{
	public class puntoesController : Controller
	{
		private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();

		// GET: puntoes
		public async Task<ActionResult> Index()
		{
			var puntos = db.puntos.Include(p => p.dispositivo);
			return View(await puntos.ToListAsync());
		}

		// GET: puntoes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			punto punto = await db.puntos.FindAsync(id);
			if (punto == null)
			{
				return HttpNotFound();
			}
			return View(punto);
		}

		// GET: puntoes/Create
		public ActionResult Create()
		{
			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo");
			return View();
		}

		// POST: puntoes/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "latitud,longitud,fecha,puntosID,dispositivoID")] punto punto)
		{
			if (ModelState.IsValid)
			{
				db.puntos.Add(punto);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", punto.dispositivoID);
			return View(punto);
		}

		// GET: puntoes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			punto punto = await db.puntos.FindAsync(id);
			if (punto == null)
			{
				return HttpNotFound();
			}
			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", punto.dispositivoID);
			return View(punto);
		}

		// POST: puntoes/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "latitud,longitud,fecha,puntosID,dispositivoID")] punto punto)
		{
			if (ModelState.IsValid)
			{
				db.Entry(punto).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", punto.dispositivoID);
			return View(punto);
		}

		// GET: puntoes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			punto punto = await db.puntos.FindAsync(id);
			if (punto == null)
			{
				return HttpNotFound();
			}
			return View(punto);
		}

		// POST: puntoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			punto punto = await db.puntos.FindAsync(id);
			db.puntos.Remove(punto);
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
