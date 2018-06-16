using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cobranzas.Data;

namespace Cobranzas.Controllers
{
	public class SeguimientoesController : Controller
	{
		private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();

		// GET: Seguimientoes
		public ActionResult Index()
		{
			return View();
		}
		public async Task<ActionResult> List()
		{
			var seguimientoes = db.Seguimientoes.Include(s => s.dispositivo).Include(s => s.Usuario);
			return PartialView("_List", await seguimientoes.ToListAsync());
		}

		public async Task<ActionResult> JsonList()
		{
			var seguimientoes = await db.Seguimientoes.Include(s => s.dispositivo).Include(s => s.Usuario).ToListAsync();
			return Json(seguimientoes);
		}

		// GET: Seguimientoes/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Seguimiento seguimiento = await db.Seguimientoes.FindAsync(id);
			if (seguimiento == null)
			{
				return HttpNotFound();
			}
			return View(seguimiento);
		}

		// GET: Seguimientoes/Create
		public ActionResult Create()
		{
			var disp = db.Seguimientoes.Where(x => x.estado == true).Select(x => x.dispositivoID).ToList();
			var user = db.Seguimientoes.Where(m => m.estado == true).Select(m => m.usuarioID).ToList();
			ViewBag.dispositivoID = new SelectList(db.dispositivoes.Where(s=>!disp.Contains(s.dispositivoID)), "dispositivoID", "modelo");
			ViewBag.usuarioID = new SelectList(db.Usuarios.Where(p=>!user.Contains(p.UsuarioID)), "UsuarioID", "UsuarioNombre");
			return View("_Create",new Seguimiento());
		}

		// POST: Seguimientoes/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "fecha,estado,seguimientoID,dispositivoID,usuarioID")] Seguimiento seguimiento)
		{
			if (ModelState.IsValid)
			{
				db.Seguimientoes.Add(seguimiento);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", seguimiento.dispositivoID);
			ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", seguimiento.usuarioID);
			return View(seguimiento);
		}

		// GET: Seguimientoes/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Seguimiento seguimiento = await db.Seguimientoes.FindAsync(id);
			if (seguimiento == null)
			{
				return HttpNotFound();
			}
			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", seguimiento.dispositivoID);
			ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", seguimiento.usuarioID);
			return View(seguimiento);
		}

		// POST: Seguimientoes/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "fecha,estado,seguimientoID,dispositivoID,usuarioID")] Seguimiento seguimiento)
		{
			if (ModelState.IsValid)
			{
				db.Entry(seguimiento).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.dispositivoID = new SelectList(db.dispositivoes, "dispositivoID", "modelo", seguimiento.dispositivoID);
			ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", seguimiento.usuarioID);
			return View(seguimiento);
		}

		// GET: Seguimientoes/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Seguimiento seguimiento = await db.Seguimientoes.FindAsync(id);
			if (seguimiento == null)
			{
				return HttpNotFound();
			}
			return View(seguimiento);
		}

		// POST: Seguimientoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Seguimiento seguimiento = await db.Seguimientoes.FindAsync(id);
			db.Seguimientoes.Remove(seguimiento);
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
