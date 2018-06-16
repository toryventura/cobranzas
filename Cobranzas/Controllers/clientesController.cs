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
using Cobranzas.Clases;

namespace Cobranzas.Controllers
{
    public class clientesController : Controller
    {
        private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();
		List<KeyValuePair<string, string>> mensaje = new List<KeyValuePair<string, string>>();
		// GET: clientes
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> List() {
			return PartialView("_List", await db.clientes.ToListAsync());
		}
        // GET: clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details",cliente);
        }

        // GET: clientes/Create
        public ActionResult Create()
        {
            return PartialView("_Create", new cliente());
        }

        // POST: clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nombre,apellido,telefono,ci,domicilio,otros,clienteID")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.clientes.Add(cliente);
                await db.SaveChangesAsync();
				mensaje.Clear();
				mensaje.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensaje);
            }

            return PartialView("_Create",cliente);
        }

        // GET: clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",cliente);
        }

        // POST: clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "nombre,apellido,telefono,ci,domicilio,otros,clienteID")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
				mensaje.Clear();
				mensaje.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensaje);
            }
            return PartialView("_Edit",cliente);
        }

        // GET: clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            cliente cliente = await db.clientes.FindAsync(id);
            db.clientes.Remove(cliente);
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
